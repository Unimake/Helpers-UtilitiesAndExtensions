using System;
using Xunit;

namespace Unimake.Helpers_UtilitiesAndExtensions.Test.ExtensionsTest
{
    public class EnumExtensionsTest
    {
        #region Public Enums

        [Flags]
        public enum EnumFlagTest
        {
            Value0 = 0,      // 000000 -> 0
            Value1 = 1 << 0, // 000001 -> 1
            Value2 = 1 << 1, // 000010 -> 2
            Value3 = 1 << 2, // 000100 -> 4
            Value4 = 1 << 3, // 001000 -> 8
            Value5 = 1 << 4, // 010000 -> 16
            Value6 = 1 << 5, // 100000 -> 32
            SomeNumbers = Value0 | Value3 | Value5,
            RandomNumbers = Value2 | Value4 | Value6,
            CompositeNumbers = Value1 | Value4 | Value6,
            AllNumbers = Value0 | Value1 | Value2 | Value3 | Value4 | Value5 | Value6
        }
        public enum EnumValueTest
        {
            Value0,
            Value1,
            Value2,
            Value3,
            Value4,
            Value5,
            Value6
        }
        public enum TestEnum
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Three = 3
        }

        [Flags]
        public enum TestFlags : short
        {
            None = 0,
            A = 1,
            B = 2,
            C = 4,
            D = 8
        }

        #endregion Public Enums

        #region Public Methods

        [Fact]
        public void IsValid_Should_ReturnFalse_ForNull()
        {
            Enum value = null;
            Assert.False(value.IsValid());
        }

        [Theory]
        [InlineData(TestFlags.A, true)]
        [InlineData(TestFlags.B | TestFlags.C, true)]
        [InlineData((TestFlags)15, true)] // A | B | C | D
        [InlineData((TestFlags)16, false)] // Bit inválido
        [InlineData((TestFlags)0, true)] // None
        [InlineData((TestFlags)99, false)]
        public void IsValid_Should_Validate_FlagsEnums(TestFlags value, bool expected)
        {
            Assert.Equal(expected, value.IsValid());
        }

        [Theory]
        [InlineData(TestEnum.One, true)]
        [InlineData(TestEnum.Three, true)]
        [InlineData((TestEnum)99, false)]
        [InlineData((TestEnum)0, true)]
        public void IsValid_Should_Validate_RegularEnums(TestEnum value, bool expected)
        {
            Assert.Equal(expected, value.IsValid());
        }

        [Fact]
        public void JoinAsIntegerTest()
        {
            var enumValues = new EnumValueTest[] { EnumValueTest.Value1, EnumValueTest.Value2, EnumValueTest.Value3, EnumValueTest.Value4, EnumValueTest.Value5, EnumValueTest.Value6 };
            var expected = "1,2,3,4,5,6";
            var result = enumValues.JoinAsInteger();
            Assert.Equal(expected, result);

            enumValues = new EnumValueTest[] { EnumValueTest.Value1, EnumValueTest.Value3, EnumValueTest.Value5 };
            expected = "1,3,5";
            result = enumValues.JoinAsInteger();
            Assert.Equal(expected, result);

            result = EnumValueTest.Value2.JoinAsInteger();
            Assert.Equal("2", result);

            var enumFlags = new EnumFlagTest[] { EnumFlagTest.CompositeNumbers };
            expected = "1,8,32";
            result = enumFlags.JoinAsInteger();
            Assert.Equal(expected, result);

            enumFlags = new EnumFlagTest[] { EnumFlagTest.Value4, EnumFlagTest.CompositeNumbers };
            expected = "8,1,8,32";
            result = enumFlags.JoinAsInteger();
            Assert.Equal(expected, result);

            enumFlags = new EnumFlagTest[] { EnumFlagTest.Value4, EnumFlagTest.CompositeNumbers, EnumFlagTest.Value6 };
            expected = "8,1,8,32,32";
            result = enumFlags.JoinAsInteger();
            Assert.Equal(expected, result);

            result = EnumFlagTest.CompositeNumbers.JoinAsInteger();
            expected = "1,8,32";
            Assert.Equal(expected, result);

            result = EnumFlagTest.RandomNumbers.JoinAsInteger();
            expected = "2,8,32";
            Assert.Equal(expected, result);

            result = EnumFlagTest.SomeNumbers.JoinAsInteger(false);
            expected = "0,4,16";
            Assert.Equal(expected, result);

            result = EnumFlagTest.AllNumbers.JoinAsInteger(false);
            expected = "0,1,2,4,8,16,32";
            Assert.Equal(expected, result);
        }

        #endregion Public Methods
    }
}