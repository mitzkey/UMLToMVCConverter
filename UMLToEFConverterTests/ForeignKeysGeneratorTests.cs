namespace UMLToEFConverterTests
{
    using System;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using Ploeh.AutoFixture;
    using UMLToEFConverter.Generators;
    using UMLToEFConverter.Generators.Deserializers.Interfaces;
    using UMLToEFConverter.Models;

    public class ForeignKeysGeneratorTests
    {
        private Fixture fixture;
        private ForeignKeysGenerator sut;
        private Mock<IPropertyDeserializer> propertyDeserializerMock;
        private AssociationEndMember sourceMember;
        private AssociationEndMember destinationMember;
        private Property propertyReturnedByDeserializer;

        [SetUp]
        public void SetUp()
        {
            this.fixture = new Fixture();
            this.fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            this.fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var typeReference = TypeReference.Builder().Build();
            this.fixture.Inject(typeReference);
            
            this.propertyDeserializerMock = new Mock<IPropertyDeserializer>();
            this.propertyReturnedByDeserializer = Property.Builder().Build();
            this.propertyDeserializerMock
                .Setup(x => x.CreateBasicProperty(
                    It.IsAny<string>(), 
                    It.IsAny<Type>(),
                    It.IsAny<Type>()))
                .Returns(this.propertyReturnedByDeserializer);

            this.sut = 
                new ForeignKeysGenerator(this.propertyDeserializerMock.Object);
        }

        [Test]
        public void When_Destination_Type_Has_Defined_Primary_Key_Then_Creates_Basic_Property_For_Each_Key()
        {
            // Arrange
            this.SetupAssociationEndMembers();
            var expectedNewPropertiesCount =
                this.destinationMember.Type.PrimaryKeyAttributes.Count;

            // Act
            this.sut.Generate(this.sourceMember, this.destinationMember);

            // Assert
            this.propertyDeserializerMock
                .Verify(x => 
                    x.CreateBasicProperty(
                        It.IsAny<string>(), 
                        It.IsAny<Type>(), 
                        It.IsAny<Type>()),
                    Times.Exactly(expectedNewPropertiesCount));
        }

        [Test]
        public void When_Creates_Basic_Properties_For_Complex_Key_Then_Adds_Proper_Foreign_Key_Property()
        {
            // Arrange
            this.SetupAssociationEndMembers();

            // Act
            this.sut.Generate(this.sourceMember, this.destinationMember);

            // Assert
            var sourceTypeProperties = this.sourceMember.Type.Properties;
            Assert.That(sourceTypeProperties.Contains(this.propertyReturnedByDeserializer));
        }

        [Test]
        public void When_Creates_Basic_Properties_For_Complex_Key_Then_Adds_Proper_Foreign_Key_Attribute_To_Navigational_Property()
        {
            // Arrange
            this.SetupAssociationEndMembers();
            var expectedForeignKeyNames = 
                string.Join(",", 
                    this.destinationMember.Type.PrimaryKeyAttributes.Select(x => this.sourceMember.Name + x.Name));

            // Act
            this.sut.Generate(this.sourceMember, this.destinationMember);

            // Assert
            var foundNavigationalProperty =
                this.sourceMember.Type.Properties.Single(x => x.Name == this.sourceMember.Name);
            Assert.That(foundNavigationalProperty.Attributes.Exists(
                x => x.Name == "ForeignKey" && x.Value == expectedForeignKeyNames));
        }

        [Test]
        public void When_Source_Member_Multiplicity_Is_Exactly_One_Then_Foreign_Key_Property_Has_Required_Attribute()
        {
            // Arrange
            this.SetupAssociationEndMembers();
            this.sourceMember.Multiplicity = Multiplicity.ExactlyOne;

            // Act
            this.sut.Generate(this.sourceMember, this.destinationMember);

            // Assert
            Assert.That(this.propertyReturnedByDeserializer.Attributes.Exists(
                x => x.Name == "Required"));
        }

        [Test]
        public void When_Source_Member_Multiplicity_Is_One_Or_More_Then_Foreign_Key_Property_Has_Required_Attribute()
        {
            // Arrange
            this.SetupAssociationEndMembers();
            this.sourceMember.Multiplicity = Multiplicity.OneOrMore;

            // Act
            this.sut.Generate(this.sourceMember, this.destinationMember);

            // Assert
            Assert.That(this.propertyReturnedByDeserializer.Attributes.Exists(
                x => x.Name == "Required"));
        }

        [Test]
        public void When_Source_Member_Multiplicity_Lower_Bound_Is_Zeor_Then_Foreign_Key_Property_Does_Not_Have_Required_Attribute()
        {
            // Arrange
            this.SetupAssociationEndMembers();
            this.sourceMember.Multiplicity = Multiplicity.ZeroOrOne;

            // Act
            this.sut.Generate(this.sourceMember, this.destinationMember);

            // Assert
            Assert.That(!this.propertyReturnedByDeserializer.Attributes.Exists(
                x => x.Name == "Required"));
        }

        [Test]
        public void When_Destination_Type_Has_No_Defined_Primary_Keys_Then_Creates_One_Foreign_Key_Property()
        {
            // Arrange
            this.SetupAssociationEndMembers();
            this.destinationMember.Type.PrimaryKeyAttributes.Clear();

            // Act
            this.sut.Generate(this.sourceMember, this.destinationMember);

            // Assert
            // Assert
            this.propertyDeserializerMock
                .Verify(x =>
                        x.CreateBasicProperty(
                            It.IsAny<string>(),
                            It.IsAny<Type>(), 
                            It.IsAny<Type>()),
                    Times.Once);
        }

        [Test]
        public void When_Destination_Type_Has_No_Defined_Primary_Keys_Then_Adds_Foreign_Key_Property_To_Source_Type()
        {
            // Arrange
            this.SetupAssociationEndMembers();
            this.destinationMember.Type.PrimaryKeyAttributes.Clear();

            // Act
            this.sut.Generate(this.sourceMember, this.destinationMember);

            // Assert
            Assert.That(this.sourceMember.Type.Properties.Contains(
                this.propertyReturnedByDeserializer));
        }

        [Test]
        public void When_Destination_Type_Has_No_Defined_Primary_Keys_Then_Adds_Foreign_Key_Attribute_To_Navigational_Property()
        {
            // Arrange
            this.SetupAssociationEndMembers();
            this.destinationMember.Type.PrimaryKeyAttributes.Clear();
            var expectedForeignKeyName = this.sourceMember.Name + "ID";

            // Act
            this.sut.Generate(this.sourceMember, this.destinationMember);

            // Assert
            var foundNavigationalProperty =
                this.sourceMember.Type.Properties.Single(
                    x => x.Name == this.sourceMember.Name);
            Assert.That(foundNavigationalProperty.Attributes.Exists(
                x => x.Name == "ForeignKey" && x.Value == expectedForeignKeyName));
        }

        private void SetupAssociationEndMembers()
        {
            this.sourceMember = this.fixture.Build<AssociationEndMember>()
                .Create();
            this.sourceMember.Type.Properties.FirstOrDefault().Name = 
                this.sourceMember.Name;
            this.destinationMember = this.fixture.Build<AssociationEndMember>()
                .Create();
        }
    }
}
