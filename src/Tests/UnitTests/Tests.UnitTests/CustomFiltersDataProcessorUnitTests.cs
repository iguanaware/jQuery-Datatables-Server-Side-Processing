﻿namespace Tests.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Dynamic;
    using JQDT.DataProcessing;
    using JQDT.DI;
    using JQDT.Exceptions;
    using JQDT.Models;
    using NUnit.Framework;
    using TestData.Data;
    using TestData.Models;

    internal class CustomFiltersDataProcessorUnitTests
    {
        private IDataProcess<AllTypesModel> filter;
        private IQueryable<AllTypesModel> data;
        private const int RangeConstant = 50;

        [SetUp]
        public void SetUp()
        {
            var resolver = new ServiceLocator();
            this.filter = resolver.GetCustomFiltersDataProcessor<AllTypesModel>();
            this.data = DataGenerator.GenerateSimpleData(5000, RangeConstant);
        }

        [Test]
        // ---------------------------------------------------------------
        [TestCase("Integer", FilterTypes.gt, "25")]
        [TestCase("Integer", FilterTypes.gte, "25")]
        [TestCase("Integer", FilterTypes.lt, "25")]
        [TestCase("Integer", FilterTypes.lte, "25")]
        [TestCase("Integer", FilterTypes.eq, "25")]
        [TestCase("Integer", FilterTypes.eq, "-25")]
        [TestCase("Integer", FilterTypes.eq, int.MaxValue)]
        [TestCase("Integer", FilterTypes.eq, int.MinValue)]
        [TestCase("NestedModel.Integer", FilterTypes.gt, "25")]
        [TestCase("NestedModel.Integer", FilterTypes.gte, "25")]
        [TestCase("NestedModel.Integer", FilterTypes.lt, "25")]
        [TestCase("NestedModel.Integer", FilterTypes.lte, "25")]
        [TestCase("NestedModel.Integer", FilterTypes.eq, "25")]
        [TestCase("NestedModel.Integer", FilterTypes.eq, "-25")]
        [TestCase("NestedModel.Integer", FilterTypes.eq, "99999999")]
        [TestCase("NestedModel.Integer", FilterTypes.eq, int.MaxValue)]
        [TestCase("NestedModel.Integer", FilterTypes.eq, int.MinValue)]
        // ---------------------------------------------------------------
        [TestCase("IntegerNullable", FilterTypes.gt, "25")]
        [TestCase("IntegerNullable", FilterTypes.gte, "25")]
        [TestCase("IntegerNullable", FilterTypes.lt, "25")]
        [TestCase("IntegerNullable", FilterTypes.lte, "25")]
        [TestCase("IntegerNullable", FilterTypes.eq, "25")]
        [TestCase("IntegerNullable", FilterTypes.eq, "99999999")]
        [TestCase("NestedModel.IntegerNullable", FilterTypes.gt, "25")]
        [TestCase("NestedModel.IntegerNullable", FilterTypes.gte, "25")]
        [TestCase("NestedModel.IntegerNullable", FilterTypes.lt, "25")]
        [TestCase("NestedModel.IntegerNullable", FilterTypes.lte, "25")]
        [TestCase("NestedModel.IntegerNullable", FilterTypes.eq, "25")]
        [TestCase("NestedModel.IntegerNullable", FilterTypes.eq, "99999999")]
        [TestCase("NestedModel.IntegerNullable", FilterTypes.eq, int.MaxValue)]
        [TestCase("NestedModel.IntegerNullable", FilterTypes.eq, int.MinValue)]
        // ---------------------------------------------------------------
#if USE_UTYPES
        [TestCase("UInt", FilterTypes.gt, "25")]
        [TestCase("UInt", FilterTypes.gte, "25")]
        [TestCase("UInt", FilterTypes.lt, "25")]
        [TestCase("UInt", FilterTypes.lte, "25")]
        [TestCase("UInt", FilterTypes.eq, "25")]
        [TestCase("UInt", FilterTypes.eq, "99999999")]
        [TestCase("NestedModel.UInt", FilterTypes.gt, "25")]
        [TestCase("NestedModel.UInt", FilterTypes.gte, "25")]
        [TestCase("NestedModel.UInt", FilterTypes.lt, "25")]
        [TestCase("NestedModel.UInt", FilterTypes.lte, "25")]
        [TestCase("NestedModel.UInt", FilterTypes.eq, "25")]
        [TestCase("NestedModel.UInt", FilterTypes.eq, "99999999")]
        [TestCase("NestedModel.UInt", FilterTypes.eq, uint.MaxValue)]
        [TestCase("NestedModel.UInt", FilterTypes.eq, uint.MinValue)]
        // ---------------------------------------------------------------
        [TestCase("UIntNullable", FilterTypes.gt, "25")]
        [TestCase("UIntNullable", FilterTypes.gte, "25")]
        [TestCase("UIntNullable", FilterTypes.lt, "25")]
        [TestCase("UIntNullable", FilterTypes.lte, "25")]
        [TestCase("UIntNullable", FilterTypes.eq, "25")]
        [TestCase("UIntNullable", FilterTypes.eq, "99999999")]
        [TestCase("UIntNullable", FilterTypes.eq, uint.MaxValue)]
        [TestCase("UIntNullable", FilterTypes.eq, uint.MinValue)]
        [TestCase("NestedModel.UIntNullable", FilterTypes.gt, "25")]
        [TestCase("NestedModel.UIntNullable", FilterTypes.gte, "25")]
        [TestCase("NestedModel.UIntNullable", FilterTypes.lt, "25")]
        [TestCase("NestedModel.UIntNullable", FilterTypes.lte, "25")]
        [TestCase("NestedModel.UIntNullable", FilterTypes.eq, "25")]
        [TestCase("NestedModel.UIntNullable", FilterTypes.eq, "99999999")]
        [TestCase("NestedModel.UIntNullable", FilterTypes.eq, uint.MaxValue)]
        [TestCase("NestedModel.UIntNullable", FilterTypes.eq, uint.MinValue)]
#endif
        // ---------------------------------------------------------------
        [TestCase("Long", FilterTypes.gt, "25")]
        [TestCase("Long", FilterTypes.gte, "25")]
        [TestCase("Long", FilterTypes.lt, "25")]
        [TestCase("Long", FilterTypes.lte, "25")]
        [TestCase("Long", FilterTypes.eq, "25")]
        [TestCase("Long", FilterTypes.eq, "-25")]
        [TestCase("Long", FilterTypes.eq, "99999999")]
        [TestCase("Long", FilterTypes.eq, "0")]
        [TestCase("Long", FilterTypes.eq, long.MaxValue)]
        [TestCase("Long", FilterTypes.eq, long.MinValue)]
        [TestCase("NestedModel.Long", FilterTypes.gt, "25")]
        [TestCase("NestedModel.Long", FilterTypes.gte, "25")]
        [TestCase("NestedModel.Long", FilterTypes.lt, "25")]
        [TestCase("NestedModel.Long", FilterTypes.lte, "25")]
        [TestCase("NestedModel.Long", FilterTypes.eq, "25")]
        [TestCase("NestedModel.Long", FilterTypes.eq, "-25")]
        [TestCase("NestedModel.Long", FilterTypes.eq, "0")]
        [TestCase("NestedModel.Long", FilterTypes.eq, long.MaxValue)]
        [TestCase("NestedModel.Long", FilterTypes.eq, long.MinValue)]
        // ---------------------------------------------------------------
        [TestCase("LongNullable", FilterTypes.gt, "25")]
        [TestCase("LongNullable", FilterTypes.gte, "25")]
        [TestCase("LongNullable", FilterTypes.lt, "25")]
        [TestCase("LongNullable", FilterTypes.lte, "25")]
        [TestCase("LongNullable", FilterTypes.eq, "25")]
        [TestCase("LongNullable", FilterTypes.eq, "-25")]
        [TestCase("LongNullable", FilterTypes.eq, "99999999")]
        [TestCase("LongNullable", FilterTypes.eq, "0")]
        [TestCase("LongNullable", FilterTypes.eq, long.MaxValue)]
        [TestCase("LongNullable", FilterTypes.eq, long.MinValue)]
        [TestCase("NestedModel.LongNullable", FilterTypes.gt, "25")]
        [TestCase("NestedModel.LongNullable", FilterTypes.gte, "25")]
        [TestCase("NestedModel.LongNullable", FilterTypes.lt, "25")]
        [TestCase("NestedModel.LongNullable", FilterTypes.lte, "25")]
        [TestCase("NestedModel.LongNullable", FilterTypes.eq, "25")]
        [TestCase("NestedModel.LongNullable", FilterTypes.eq, "-25")]
        [TestCase("NestedModel.LongNullable", FilterTypes.eq, "0")]
        [TestCase("NestedModel.LongNullable", FilterTypes.eq, long.MaxValue)]
        [TestCase("NestedModel.LongNullable", FilterTypes.eq, long.MinValue)]
        // ---------------------------------------------------------------
#if USE_UTYPES
        [TestCase("ULong", FilterTypes.gt, "25")]
        [TestCase("ULong", FilterTypes.gte, "25")]
        [TestCase("ULong", FilterTypes.lt, "25")]
        [TestCase("ULong", FilterTypes.lte, "25")]
        [TestCase("ULong", FilterTypes.eq, "25")]
        [TestCase("ULong", FilterTypes.eq, "99999999")]
        [TestCase("ULong", FilterTypes.eq, "0")]
        [TestCase("ULong", FilterTypes.eq, ulong.MaxValue)]
        [TestCase("ULong", FilterTypes.eq, ulong.MinValue)]
        [TestCase("NestedModel.ULong", FilterTypes.gt, "25")]
        [TestCase("NestedModel.ULong", FilterTypes.gte, "25")]
        [TestCase("NestedModel.ULong", FilterTypes.lt, "25")]
        [TestCase("NestedModel.ULong", FilterTypes.lte, "25")]
        [TestCase("NestedModel.ULong", FilterTypes.eq, "25")]
        [TestCase("NestedModel.ULong", FilterTypes.eq, "0")]
        [TestCase("NestedModel.ULong", FilterTypes.eq, ulong.MaxValue)]
        [TestCase("NestedModel.ULong", FilterTypes.eq, ulong.MinValue)]
        // ---------------------------------------------------------------
        [TestCase("ULongNullable", FilterTypes.gt, "25")]
        [TestCase("ULongNullable", FilterTypes.gte, "25")]
        [TestCase("ULongNullable", FilterTypes.lt, "25")]
        [TestCase("ULongNullable", FilterTypes.lte, "25")]
        [TestCase("ULongNullable", FilterTypes.eq, "25")]
        [TestCase("ULongNullable", FilterTypes.eq, "99999999")]
        [TestCase("ULongNullable", FilterTypes.eq, "0")]
        [TestCase("ULongNullable", FilterTypes.eq, ulong.MaxValue)]
        [TestCase("ULongNullable", FilterTypes.eq, ulong.MinValue)]
        [TestCase("NestedModel.ULongNullable", FilterTypes.gt, "25")]
        [TestCase("NestedModel.ULongNullable", FilterTypes.gte, "25")]
        [TestCase("NestedModel.ULongNullable", FilterTypes.lt, "25")]
        [TestCase("NestedModel.ULongNullable", FilterTypes.lte, "25")]
        [TestCase("NestedModel.ULongNullable", FilterTypes.eq, "25")]
        [TestCase("NestedModel.ULongNullable", FilterTypes.eq, "0")]
        [TestCase("NestedModel.ULongNullable", FilterTypes.eq, ulong.MaxValue)]
        [TestCase("NestedModel.ULongNullable", FilterTypes.eq, ulong.MinValue)]
#endif
        // ---------------------------------------------------------------
        [TestCase("Short", FilterTypes.gt, "25")]
        [TestCase("Short", FilterTypes.gte, "25")]
        [TestCase("Short", FilterTypes.lt, "25")]
        [TestCase("Short", FilterTypes.lte, "25")]
        [TestCase("Short", FilterTypes.eq, "25")]
        [TestCase("Short", FilterTypes.eq, "0")]
        [TestCase("Short", FilterTypes.eq, short.MaxValue)]
        [TestCase("Short", FilterTypes.eq, short.MinValue)]
        [TestCase("NestedModel.Short", FilterTypes.gt, "25")]
        [TestCase("NestedModel.Short", FilterTypes.gte, "25")]
        [TestCase("NestedModel.Short", FilterTypes.lt, "25")]
        [TestCase("NestedModel.Short", FilterTypes.lte, "25")]
        [TestCase("NestedModel.Short", FilterTypes.eq, "25")]
        [TestCase("NestedModel.Short", FilterTypes.eq, "0")]
        [TestCase("NestedModel.Short", FilterTypes.eq, short.MaxValue)]
        [TestCase("NestedModel.Short", FilterTypes.eq, short.MinValue)]
        // ---------------------------------------------------------------
        [TestCase("ShortNullable", FilterTypes.gt, "25")]
        [TestCase("ShortNullable", FilterTypes.gte, "25")]
        [TestCase("ShortNullable", FilterTypes.lt, "25")]
        [TestCase("ShortNullable", FilterTypes.lte, "25")]
        [TestCase("ShortNullable", FilterTypes.eq, "25")]
        [TestCase("ShortNullable", FilterTypes.eq, "-25")]
        [TestCase("ShortNullable", FilterTypes.eq, "0")]
        [TestCase("ShortNullable", FilterTypes.eq, short.MaxValue)]
        [TestCase("ShortNullable", FilterTypes.eq, short.MinValue)]
        [TestCase("NestedModel.ShortNullable", FilterTypes.gt, "25")]
        [TestCase("NestedModel.ShortNullable", FilterTypes.gte, "25")]
        [TestCase("NestedModel.ShortNullable", FilterTypes.lt, "25")]
        [TestCase("NestedModel.ShortNullable", FilterTypes.lte, "25")]
        [TestCase("NestedModel.ShortNullable", FilterTypes.eq, "25")]
        [TestCase("NestedModel.ShortNullable", FilterTypes.eq, "-25")]
        [TestCase("NestedModel.ShortNullable", FilterTypes.eq, "0")]
        [TestCase("NestedModel.ShortNullable", FilterTypes.eq, short.MaxValue)]
        [TestCase("NestedModel.ShortNullable", FilterTypes.eq, short.MinValue)]
#if USE_UTYPES
        // ---------------------------------------------------------------
        [TestCase("UShort", FilterTypes.gt, "25")]
        [TestCase("UShort", FilterTypes.gte, "25")]
        [TestCase("UShort", FilterTypes.lt, "25")]
        [TestCase("UShort", FilterTypes.lte, "25")]
        [TestCase("UShort", FilterTypes.eq, "25")]
        [TestCase("UShort", FilterTypes.eq, "9999")]
        [TestCase("UShort", FilterTypes.eq, "0")]
        [TestCase("UShort", FilterTypes.eq, ushort.MaxValue)]
        [TestCase("UShort", FilterTypes.eq, ushort.MinValue)]
        [TestCase("NestedModel.UShort", FilterTypes.gt, "25")]
        [TestCase("NestedModel.UShort", FilterTypes.gte, "25")]
        [TestCase("NestedModel.UShort", FilterTypes.lt, "25")]
        [TestCase("NestedModel.UShort", FilterTypes.lte, "25")]
        [TestCase("NestedModel.UShort", FilterTypes.eq, "25")]
        [TestCase("NestedModel.UShort", FilterTypes.eq, "0")]
        [TestCase("NestedModel.UShort", FilterTypes.eq, ushort.MaxValue)]
        [TestCase("NestedModel.UShort", FilterTypes.eq, ushort.MinValue)]
        // ---------------------------------------------------------------
        [TestCase("UShortNullable", FilterTypes.gt, "25")]
        [TestCase("UShortNullable", FilterTypes.gte, "25")]
        [TestCase("UShortNullable", FilterTypes.lt, "25")]
        [TestCase("UShortNullable", FilterTypes.lte, "25")]
        [TestCase("UShortNullable", FilterTypes.eq, "25")]
        [TestCase("UShortNullable", FilterTypes.eq, "9999")]
        [TestCase("UShortNullable", FilterTypes.eq, ushort.MaxValue)]
        [TestCase("UShortNullable", FilterTypes.eq, ushort.MinValue)]
        [TestCase("NestedModel.UShortNullable", FilterTypes.gt, "25")]
        [TestCase("NestedModel.UShortNullable", FilterTypes.gte, "25")]
        [TestCase("NestedModel.UShortNullable", FilterTypes.lt, "25")]
        [TestCase("NestedModel.UShortNullable", FilterTypes.lte, "25")]
        [TestCase("NestedModel.UShortNullable", FilterTypes.eq, "25")]
        [TestCase("NestedModel.UShortNullable", FilterTypes.eq, "9999")]
        [TestCase("NestedModel.UShortNullable", FilterTypes.eq, ushort.MaxValue)]
        [TestCase("NestedModel.UShortNullable", FilterTypes.eq, ushort.MinValue)]
#endif
        // ---------------------------------------------------------------
        [TestCase("ByteProperty", FilterTypes.gt, "25")]
        [TestCase("ByteProperty", FilterTypes.gte, "25")]
        [TestCase("ByteProperty", FilterTypes.lt, "25")]
        [TestCase("ByteProperty", FilterTypes.lte, "25")]
        [TestCase("ByteProperty", FilterTypes.eq, "25")]
        [TestCase("ByteProperty", FilterTypes.eq, "0")]
        [TestCase("ByteProperty", FilterTypes.eq, byte.MaxValue)]
        [TestCase("ByteProperty", FilterTypes.eq, byte.MinValue)]
        [TestCase("NestedModel.ByteProperty", FilterTypes.gt, "25")]
        [TestCase("NestedModel.ByteProperty", FilterTypes.gte, "25")]
        [TestCase("NestedModel.ByteProperty", FilterTypes.lt, "25")]
        [TestCase("NestedModel.ByteProperty", FilterTypes.lte, "25")]
        [TestCase("NestedModel.ByteProperty", FilterTypes.eq, "25")]
        [TestCase("NestedModel.ByteProperty", FilterTypes.eq, "0")]
        [TestCase("NestedModel.ByteProperty", FilterTypes.eq, byte.MaxValue)]
        [TestCase("NestedModel.ByteProperty", FilterTypes.eq, byte.MinValue)]
        // ---------------------------------------------------------------
        [TestCase("ByteNullable", FilterTypes.gt, "25")]
        [TestCase("ByteNullable", FilterTypes.gte, "25")]
        [TestCase("ByteNullable", FilterTypes.lt, "25")]
        [TestCase("ByteNullable", FilterTypes.lte, "25")]
        [TestCase("ByteNullable", FilterTypes.eq, "25")]
        [TestCase("ByteNullable", FilterTypes.eq, "0")]
        [TestCase("ByteNullable", FilterTypes.eq, byte.MaxValue)]
        [TestCase("ByteNullable", FilterTypes.eq, byte.MinValue)]
        [TestCase("NestedModel.ByteNullable", FilterTypes.gt, "25")]
        [TestCase("NestedModel.ByteNullable", FilterTypes.gte, "25")]
        [TestCase("NestedModel.ByteNullable", FilterTypes.lt, "25")]
        [TestCase("NestedModel.ByteNullable", FilterTypes.lte, "25")]
        [TestCase("NestedModel.ByteNullable", FilterTypes.eq, "25")]
        [TestCase("NestedModel.ByteNullable", FilterTypes.eq, "0")]
        [TestCase("NestedModel.ByteNullable", FilterTypes.eq, byte.MaxValue)]
        [TestCase("NestedModel.ByteNullable", FilterTypes.eq, byte.MinValue)]
#if USE_STYPES
        // ---------------------------------------------------------------
        [TestCase("SByteProperty", FilterTypes.gt, "25")]
        [TestCase("SByteProperty", FilterTypes.gte, "25")]
        [TestCase("SByteProperty", FilterTypes.lt, "25")]
        [TestCase("SByteProperty", FilterTypes.lte, "25")]
        [TestCase("SByteProperty", FilterTypes.eq, "25")]
        [TestCase("SByteProperty", FilterTypes.eq, "-25")]
        [TestCase("SByteProperty", FilterTypes.eq, "0")]
        [TestCase("SByteProperty", FilterTypes.eq, sbyte.MaxValue)]
        [TestCase("SByteProperty", FilterTypes.eq, sbyte.MinValue)]
        [TestCase("NestedModel.SByteProperty", FilterTypes.gt, "25")]
        [TestCase("NestedModel.SByteProperty", FilterTypes.gte, "25")]
        [TestCase("NestedModel.SByteProperty", FilterTypes.lt, "25")]
        [TestCase("NestedModel.SByteProperty", FilterTypes.lte, "25")]
        [TestCase("NestedModel.SByteProperty", FilterTypes.eq, "25")]
        [TestCase("NestedModel.SByteProperty", FilterTypes.eq, "-25")]
        [TestCase("NestedModel.SByteProperty", FilterTypes.eq, "0")]
        [TestCase("NestedModel.SByteProperty", FilterTypes.eq, sbyte.MaxValue)]
        [TestCase("NestedModel.SByteProperty", FilterTypes.eq, sbyte.MinValue)]
        // ---------------------------------------------------------------
        [TestCase("SByteNullable", FilterTypes.gt, "25")]
        [TestCase("SByteNullable", FilterTypes.gte, "25")]
        [TestCase("SByteNullable", FilterTypes.lt, "25")]
        [TestCase("SByteNullable", FilterTypes.lte, "25")]
        [TestCase("SByteNullable", FilterTypes.eq, "25")]
        [TestCase("SByteNullable", FilterTypes.eq, "-25")]
        [TestCase("SByteNullable", FilterTypes.eq, "0")]
        [TestCase("SByteNullable", FilterTypes.eq, sbyte.MaxValue)]
        [TestCase("SByteNullable", FilterTypes.eq, sbyte.MinValue)]
        [TestCase("NestedModel.SByteNullable", FilterTypes.gt, "25")]
        [TestCase("NestedModel.SByteNullable", FilterTypes.gte, "25")]
        [TestCase("NestedModel.SByteNullable", FilterTypes.lt, "25")]
        [TestCase("NestedModel.SByteNullable", FilterTypes.lte, "25")]
        [TestCase("NestedModel.SByteNullable", FilterTypes.eq, "25")]
        [TestCase("NestedModel.SByteNullable", FilterTypes.eq, "-25")]
        [TestCase("NestedModel.SByteNullable", FilterTypes.eq, "0")]
        [TestCase("NestedModel.SByteNullable", FilterTypes.eq, sbyte.MaxValue)]
        [TestCase("NestedModel.SByteNullable", FilterTypes.eq, sbyte.MinValue)]
#endif
        // ---------------------------------------------------------------
        [TestCase("DoubleProperty", FilterTypes.gt, "25.1543254325")]
        [TestCase("DoubleProperty", FilterTypes.gte, "25.1543254325")]
        [TestCase("DoubleProperty", FilterTypes.lt, "25.1543254325")]
        [TestCase("DoubleProperty", FilterTypes.lte, "25.1543254325")]
        [TestCase("DoubleProperty", FilterTypes.eq, "25.1543254325")]
        [TestCase("DoubleProperty", FilterTypes.eq, "-25.1543254325")]
        [TestCase("DoubleProperty", FilterTypes.eq, "0")]
        [TestCase("DoubleProperty", FilterTypes.eq, 1.7976931348623157)]
        [TestCase("DoubleProperty", FilterTypes.eq, -1.7976931348623157)]
        [TestCase("NestedModel.DoubleProperty", FilterTypes.gt, "25.1543254325")]
        [TestCase("NestedModel.DoubleProperty", FilterTypes.gte, "25.1543254325")]
        [TestCase("NestedModel.DoubleProperty", FilterTypes.lt, "25.1543254325")]
        [TestCase("NestedModel.DoubleProperty", FilterTypes.lte, "25.1543254325")]
        [TestCase("NestedModel.DoubleProperty", FilterTypes.eq, "25.1543254325")]
        [TestCase("NestedModel.DoubleProperty", FilterTypes.eq, "-25.1543254325")]
        [TestCase("NestedModel.DoubleProperty", FilterTypes.eq, "0")]
        [TestCase("NestedModel.DoubleProperty", FilterTypes.eq, 1.7976931348623157)]
        [TestCase("NestedModel.DoubleProperty", FilterTypes.eq, -1.7976931348623157)]
        // ---------------------------------------------------------------
        [TestCase("DoubleNullable", FilterTypes.gt, "25.1543254325")]
        [TestCase("DoubleNullable", FilterTypes.gte, "25.1543254325")]
        [TestCase("DoubleNullable", FilterTypes.lt, "25.1543254325")]
        [TestCase("DoubleNullable", FilterTypes.lte, "25.1543254325")]
        [TestCase("DoubleNullable", FilterTypes.eq, "25.1543254325")]
        [TestCase("DoubleNullable", FilterTypes.eq, "-25.1543254325")]
        [TestCase("DoubleNullable", FilterTypes.eq, "0")]
        [TestCase("DoubleNullable", FilterTypes.eq, 1.7976931348623157)]
        [TestCase("DoubleNullable", FilterTypes.eq, -1.7976931348623157)]
        [TestCase("NestedModel.DoubleNullable", FilterTypes.gt, "25.1543254325")]
        [TestCase("NestedModel.DoubleNullable", FilterTypes.gte, "25.1543254325")]
        [TestCase("NestedModel.DoubleNullable", FilterTypes.lt, "25.1543254325")]
        [TestCase("NestedModel.DoubleNullable", FilterTypes.lte, "25.1543254325")]
        [TestCase("NestedModel.DoubleNullable", FilterTypes.eq, "25.1543254325")]
        [TestCase("NestedModel.DoubleNullable", FilterTypes.eq, "-25.1543254325")]
        [TestCase("NestedModel.DoubleNullable", FilterTypes.eq, "0")]
        [TestCase("NestedModel.DoubleNullable", FilterTypes.eq, 1.7976931348623157)]
        [TestCase("NestedModel.DoubleNullable", FilterTypes.eq, -1.7976931348623157)]
        // ---------------------------------------------------------------
        [TestCase(nameof(AllTypesModel.Float), FilterTypes.gt, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.Float), FilterTypes.gte, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.Float), FilterTypes.lt, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.Float), FilterTypes.lte, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.Float), FilterTypes.eq, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.Float), FilterTypes.eq, "-25.1543254325")]
        [TestCase(nameof(AllTypesModel.Float), FilterTypes.eq, "0")]
        [TestCase(nameof(AllTypesModel.Float), FilterTypes.eq, 1.7976931348623157)]
        [TestCase(nameof(AllTypesModel.Float), FilterTypes.eq, -1.7976931348623157)]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.Float), FilterTypes.gt, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.Float), FilterTypes.gte, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.Float), FilterTypes.lt, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.Float), FilterTypes.lte, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.Float), FilterTypes.eq, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.Float), FilterTypes.eq, "-25.1543254325")]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.Float), FilterTypes.eq, "0")]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.Float), FilterTypes.eq, 1.7976931348623157)]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.Float), FilterTypes.eq, -1.7976931348623157)]
        // ---------------------------------------------------------------
        [TestCase(nameof(AllTypesModel.FloatNullable), FilterTypes.gt, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.FloatNullable), FilterTypes.gte, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.FloatNullable), FilterTypes.lt, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.FloatNullable), FilterTypes.lte, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.FloatNullable), FilterTypes.eq, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.FloatNullable), FilterTypes.eq, "-25.1543254325")]
        [TestCase(nameof(AllTypesModel.FloatNullable), FilterTypes.eq, "0")]
        [TestCase(nameof(AllTypesModel.FloatNullable), FilterTypes.eq, 1.7976931348623157)]
        [TestCase(nameof(AllTypesModel.FloatNullable), FilterTypes.eq, -1.7976931348623157)]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.FloatNullable), FilterTypes.gt, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.FloatNullable), FilterTypes.gte, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.FloatNullable), FilterTypes.lt, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.FloatNullable), FilterTypes.lte, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.FloatNullable), FilterTypes.eq, "25.1543254325")]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.FloatNullable), FilterTypes.eq, "-25.1543254325")]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.FloatNullable), FilterTypes.eq, "0")]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.FloatNullable), FilterTypes.eq, 1.7976931348623157)]
        [TestCase(nameof(AllTypesModel.NestedModel) + "." + nameof(AllTypesModel.FloatNullable), FilterTypes.eq, -1.7976931348623157)]
        // ---------------------------------------------------------------
        [TestCase("DecimalProperty", FilterTypes.gt, "25.1543254325")]
        [TestCase("DecimalProperty", FilterTypes.gte, "25.1543254325")]
        [TestCase("DecimalProperty", FilterTypes.lt, "25.1543254325")]
        [TestCase("DecimalProperty", FilterTypes.lte, "25.1543254325")]
        [TestCase("DecimalProperty", FilterTypes.eq, "25.1543254325")]
        [TestCase("DecimalProperty", FilterTypes.eq, "-25.1543254325")]
        [TestCase("DecimalProperty", FilterTypes.eq, "0")]
        [TestCase("NestedModel.DecimalProperty", FilterTypes.gt, "25.1543254325")]
        [TestCase("NestedModel.DecimalProperty", FilterTypes.gte, "25.1543254325")]
        [TestCase("NestedModel.DecimalProperty", FilterTypes.lt, "25.1543254325")]
        [TestCase("NestedModel.DecimalProperty", FilterTypes.lte, "25.1543254325")]
        [TestCase("NestedModel.DecimalProperty", FilterTypes.eq, "25.1543254325")]
        [TestCase("NestedModel.DecimalProperty", FilterTypes.eq, "-25.1543254325")]
        [TestCase("NestedModel.DecimalProperty", FilterTypes.eq, "0")]
        // ---------------------------------------------------------------
        [TestCase("DecimalNullable", FilterTypes.gt, "25.1543254325")]
        [TestCase("DecimalNullable", FilterTypes.gte, "25.1543254325")]
        [TestCase("DecimalNullable", FilterTypes.lt, "25.1543254325")]
        [TestCase("DecimalNullable", FilterTypes.lte, "25.1543254325")]
        [TestCase("DecimalNullable", FilterTypes.eq, "25.1543254325")]
        [TestCase("DecimalNullable", FilterTypes.eq, "-25.1543254325")]
        [TestCase("DecimalNullable", FilterTypes.eq, "0")]
        [TestCase("NestedModel.DecimalNullable", FilterTypes.gt, "25.1543254325")]
        [TestCase("NestedModel.DecimalNullable", FilterTypes.gte, "25.1543254325")]
        [TestCase("NestedModel.DecimalNullable", FilterTypes.lt, "25.1543254325")]
        [TestCase("NestedModel.DecimalNullable", FilterTypes.lte, "25.1543254325")]
        [TestCase("NestedModel.DecimalNullable", FilterTypes.eq, "25.1543254325")]
        [TestCase("NestedModel.DecimalNullable", FilterTypes.eq, "-25.1543254325")]
        [TestCase("NestedModel.DecimalNullable", FilterTypes.eq, "0")]
        // ---------------------------------------------------------------
        [TestCase("BooleanProperty", FilterTypes.gt, typeof(InvalidTypeForOperationException))]
        [TestCase("BooleanProperty", FilterTypes.gte, typeof(InvalidTypeForOperationException))]
        [TestCase("BooleanProperty", FilterTypes.lt, typeof(InvalidTypeForOperationException))]
        [TestCase("BooleanProperty", FilterTypes.lte, typeof(InvalidTypeForOperationException))]
        [TestCase("BooleanProperty", FilterTypes.eq, "true")]
        [TestCase("BooleanProperty", FilterTypes.eq, "fALse")]
        [TestCase("NestedModel.BooleanProperty", FilterTypes.gt, typeof(InvalidTypeForOperationException))]
        [TestCase("NestedModel.BooleanProperty", FilterTypes.gte, typeof(InvalidTypeForOperationException))]
        [TestCase("NestedModel.BooleanProperty", FilterTypes.lt, typeof(InvalidTypeForOperationException))]
        [TestCase("NestedModel.BooleanProperty", FilterTypes.lte, typeof(InvalidTypeForOperationException))]
        [TestCase("NestedModel.BooleanProperty", FilterTypes.eq, "true")]
        [TestCase("NestedModel.BooleanProperty", FilterTypes.eq, "fALse")]
        // ---------------------------------------------------------------
        [TestCase("BooleanNullable", FilterTypes.gt, typeof(InvalidTypeForOperationException))]
        [TestCase("BooleanNullable", FilterTypes.gte, typeof(InvalidTypeForOperationException))]
        [TestCase("BooleanNullable", FilterTypes.lt, typeof(InvalidTypeForOperationException))]
        [TestCase("BooleanNullable", FilterTypes.lte, typeof(InvalidTypeForOperationException))]
        [TestCase("BooleanNullable", FilterTypes.eq, "true")]
        [TestCase("BooleanNullable", FilterTypes.eq, "fALse")]
        [TestCase("NestedModel.BooleanNullable", FilterTypes.gt, typeof(InvalidTypeForOperationException))]
        [TestCase("NestedModel.BooleanNullable", FilterTypes.gte, typeof(InvalidTypeForOperationException))]
        [TestCase("NestedModel.BooleanNullable", FilterTypes.lt, typeof(InvalidTypeForOperationException))]
        [TestCase("NestedModel.BooleanNullable", FilterTypes.lte, typeof(InvalidTypeForOperationException))]
        [TestCase("NestedModel.BooleanNullable", FilterTypes.eq, "true")]
        [TestCase("NestedModel.BooleanNullable", FilterTypes.eq, "fALse")]
        // ---------------------------------------------------------------
#if USE_CHARTYPE
        [TestCase("CharProperty", FilterTypes.gt, "n", @"'n'")]
        [TestCase("CharProperty", FilterTypes.gte, "n", @"'n'")]
        [TestCase("CharProperty", FilterTypes.lt, 'n', @"'n'")]
        [TestCase("CharProperty", FilterTypes.lte, 'n', @"'n'")]
        [TestCase("CharProperty", FilterTypes.eq, "n", @"'n'")]
        [TestCase("NestedModel.CharProperty", FilterTypes.gt, "n", @"'n'")]
        [TestCase("NestedModel.CharProperty", FilterTypes.gte, "n", @"'n'")]
        [TestCase("NestedModel.CharProperty", FilterTypes.lt, 'n', @"'n'")]
        [TestCase("NestedModel.CharProperty", FilterTypes.lte, 'n', @"'n'")]
        [TestCase("NestedModel.CharProperty", FilterTypes.eq, "n", @"'n'")]
#endif
#if USE_CHARTYPE
        // ---------------------------------------------------------------
        [TestCase("CharNullable", FilterTypes.gt, "n", @"'n'")]
        [TestCase("CharNullable", FilterTypes.gte, "n", @"'n'")]
        [TestCase("CharNullable", FilterTypes.lt, 'n', @"'n'")]
        [TestCase("CharNullable", FilterTypes.lte, 'n', @"'n'")]
        [TestCase("CharNullable", FilterTypes.eq, "n", @"'n'")]
        [TestCase("NestedModel.CharNullable", FilterTypes.gt, "n", @"'n'")]
        [TestCase("NestedModel.CharNullable", FilterTypes.gte, "n", @"'n'")]
        [TestCase("NestedModel.CharNullable", FilterTypes.lt, 'n', @"'n'")]
        [TestCase("NestedModel.CharNullable", FilterTypes.lte, 'n', @"'n'")]
        [TestCase("NestedModel.CharNullable", FilterTypes.eq, "n", @"'n'")]
#endif
        // ---------------------------------------------------------------
        [TestCase("StringProperty", FilterTypes.gt, typeof(InvalidTypeForOperationException))]
        [TestCase("StringProperty", FilterTypes.gte, typeof(InvalidTypeForOperationException))]
        [TestCase("StringProperty", FilterTypes.lt, typeof(InvalidTypeForOperationException))]
        [TestCase("StringProperty", FilterTypes.lte, typeof(InvalidTypeForOperationException))]
        [TestCase("StringProperty", FilterTypes.eq, "z", "\"z\"")]
        public void CustomFilters_ShouldWorkProperlyForRangeWithAllSupportedTypes(string column, FilterTypes filterType, object value, string valueFormatOnAssert = null)
        {
            var requestModel = new RequestInfoModel
            {
                TableParameters = new DataTableAjaxPostModel
                {
                    Custom = new Custom
                    {
                        Filters = new Dictionary<string, IEnumerable<FilterModel>>
                        {
                            {
                                column, new List<FilterModel>
                                {
                                    new FilterModel { Type = filterType, Value = value.ToString() }
                                }
                            }
                        }
                    }
                }
            };

            var typeValue = value as Type;
            if (typeValue != null && typeValue == typeof(InvalidTypeForOperationException))
            {
                Assert.Throws<InvalidTypeForOperationException>(() =>
                {
                    var tmp = filter.ProcessData(data, requestModel);
                });

                return;
            }

            var processedData = filter.ProcessData(data, requestModel);
            var predicate = $"{column} {this.GetFilterCsRepresentation(filterType)} {valueFormatOnAssert ?? value.ToString()}";
            var expectedData = data.ToList().Where(predicate);

            Trace.WriteLine($"Number of results: {processedData.Count()}");
            Assert.AreEqual(processedData.Count(), expectedData.Count());
        }

        [Test]
        [TestCase(nameof(AllTypesModel.DateTimeProperty), FilterTypes.gt)]
        [TestCase(nameof(AllTypesModel.DateTimeProperty), FilterTypes.lt)]
        [TestCase(nameof(AllTypesModel.DateTimeProperty), FilterTypes.gte)]
        [TestCase(nameof(AllTypesModel.DateTimeProperty), FilterTypes.lte)]
        [TestCase(nameof(AllTypesModel.DateTimeProperty), FilterTypes.eq)]
        // -----------------------------------------------------------------
        [TestCase(nameof(AllTypesModel.DateTimeNullable), FilterTypes.gt)]
        [TestCase(nameof(AllTypesModel.DateTimeNullable), FilterTypes.lt)]
        [TestCase(nameof(AllTypesModel.DateTimeNullable), FilterTypes.gte)]
        [TestCase(nameof(AllTypesModel.DateTimeNullable), FilterTypes.lte)]
        [TestCase(nameof(AllTypesModel.DateTimeNullable), FilterTypes.eq)]
        // -----------------------------------------------------------------
        [TestCase(nameof(AllTypesModel.DateTimeOffsetProperty), FilterTypes.gt)]
        [TestCase(nameof(AllTypesModel.DateTimeOffsetProperty), FilterTypes.lt)]
        [TestCase(nameof(AllTypesModel.DateTimeOffsetProperty), FilterTypes.gte)]
        [TestCase(nameof(AllTypesModel.DateTimeOffsetProperty), FilterTypes.lte)]
        [TestCase(nameof(AllTypesModel.DateTimeOffsetProperty), FilterTypes.eq)]
        // -----------------------------------------------------------------
        [TestCase(nameof(AllTypesModel.DateTimeOffsetNullable), FilterTypes.gt)]
        [TestCase(nameof(AllTypesModel.DateTimeOffsetNullable), FilterTypes.lt)]
        [TestCase(nameof(AllTypesModel.DateTimeOffsetNullable), FilterTypes.gte)]
        [TestCase(nameof(AllTypesModel.DateTimeOffsetNullable), FilterTypes.lte)]
        [TestCase(nameof(AllTypesModel.DateTimeOffsetNullable), FilterTypes.eq)]
        public void CustomFilters_ShouldWorkProperlyForRangeWithDateTimes(string column, FilterTypes filterType)
        {
            var random = new Random();
            var dateToCompare = this.data.ToList()[random.Next(0, data.Count() - 1)].DateTimeProperty;
            var value = dateToCompare.ToString();

            var requestModel = new RequestInfoModel
            {
                TableParameters = new DataTableAjaxPostModel
                {
                    Custom = new Custom
                    {
                        Filters = new Dictionary<string, IEnumerable<FilterModel>>
                        {
                            {
                                column, new List<FilterModel>
                                {
                                    new FilterModel { Type = filterType, Value = value.ToString() }
                                }
                            }
                        }
                    }
                }
            };

            var processedData = filter.ProcessData(data, requestModel);
            string predicate = null;
            var propType = typeof(AllTypesModel).GetProperty(column).PropertyType;
            var propTypeGenericName = propType.IsGenericType ? propType.GenericTypeArguments.First().Name : propType.Name;
            if (filterType != FilterTypes.eq)
            {
                predicate = $"{column} {this.GetFilterCsRepresentation(filterType)} {propTypeGenericName}.Parse(\"{value}\")";
            }
            else
            {
                if (propType.IsGenericType)
                {
                    predicate = $"{column}.HasValue && {column}.Value.Year == {dateToCompare.Year} && {column}.Value.Month == {dateToCompare.Month} && {column}.Value.Day == {dateToCompare.Day} && {column}.Value.Hour == {dateToCompare.Hour} && {column}.Value.Minute == {dateToCompare.Minute} && {column}.Value.Second == {dateToCompare.Second}";
                }
                else
                {
                    predicate = $"{column} != null && {column}.Year == {dateToCompare.Year} && {column}.Month == {dateToCompare.Month} && {column}.Day == {dateToCompare.Day} && {column}.Hour == {dateToCompare.Hour} && {column}.Minute == {dateToCompare.Minute} && {column}.Second == {dateToCompare.Second}";
                }
            }

            var expectedData = data.ToList().Where(predicate);

            Trace.WriteLine($"Number of results: {processedData.Count()}");
            Assert.AreEqual(processedData.Count(), expectedData.Count());
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void CustomFilters_ShouldWorkProperlyWithMultipleFilters(int testCaseKey)
        {
            var min = (RangeConstant / 5).ToString();
            var max = (RangeConstant / 2).ToString();

            var testCases = new Dictionary<int, Custom>
            {
                {
                    1,
                    new Custom
                    {
                        Filters = new Dictionary<string, IEnumerable<FilterModel>>
                        {
                            {
                                "Integer",
                                new List<FilterModel>
                                {
                                    new FilterModel {Type = FilterTypes.gt, Value = min},
                                    new FilterModel {Type = FilterTypes.lt, Value = max},
                                }
                            }
                        }
                    }
                },

                {
                    2,
                    new Custom
                    {
                        Filters = new Dictionary<string, IEnumerable<FilterModel>>
                        {
                            {
                                "Integer",
                                new List<FilterModel>
                                {
                                    new FilterModel {Type = FilterTypes.gt, Value = min},
                                    new FilterModel {Type = FilterTypes.lt, Value = max},
                                }
                            },
                            {
                                "CharProperty",
                                new List<FilterModel>
                                {
                                    new FilterModel {Type = FilterTypes.eq, Value = "a" },
                                }
                            }
                        }
                    }
                }
            };

            var predicates = new Dictionary<int, string>
            {
                { 1, $"Integer > {min} && Integer < {max}" },
#if USE_CHARTYPE
                { 2, $"Integer > {min} && Integer < {max} && CharProperty == 'a'" },
#else
                { 2, $"Integer > {min} && Integer < {max} && CharProperty == \"a\"" },
#endif
            };

            var custom = testCases[testCaseKey];

            var requestModel = new RequestInfoModel
            {
                TableParameters = new DataTableAjaxPostModel
                {
                    Custom = custom
                }
            };

            var processedData = filter.ProcessData(data, requestModel);
            var predicate = predicates[testCaseKey];
            var expectedData = data.ToList().Where(predicate);

            Trace.WriteLine($"Number of results: {processedData.Count()}");
            Assert.AreEqual(processedData.Count(), expectedData.Count());
        }

        private string GetFilterCsRepresentation(FilterTypes filterType)
        {
            switch (filterType)
            {
                case FilterTypes.gte:
                    return ">=";

                case FilterTypes.gt:
                    return ">";

                case FilterTypes.lt:
                    return "<";

                case FilterTypes.lte:
                    return "<=";

                case FilterTypes.eq:
                    return "==";

                default:
                    throw new ArgumentException();
            }
        }
    }
}