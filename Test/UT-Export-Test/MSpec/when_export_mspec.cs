using System.Collections.Generic;
using System.Linq;
using UTExport.MSpec;
using Xunit;

namespace UTExport.Test.MSpec
{
    public class when_export_mspec
    {
        [Fact]
        public void when_export_standard_simple_mspec()
        {
            var mSpecManager = new MSpecManager();
            List<UTInfo> utInfos =
                mSpecManager.Export(
                    Utils.GetFixtureFileFullName("MSpec\\Fixtures\\StandardSimpleMSpec.txt"));

            Assert.Equal(1, utInfos.Count);
            UTInfo utInfo = utInfos.Single();
            Assert.Equal("StandardSimpleMSpec.txt", utInfo.FileName);
            Assert.Equal(1, utInfo.WhenList.Count);
            Assert.Equal("do_something", utInfo.WhenList.Single());
            Assert.Equal(2, utInfo.ThenList.Count);
            Assert.Equal("should_do_one_thing", utInfo.ThenList.First());
            Assert.Equal("should_do_another_thing", utInfo.ThenList.Last());
        }

//        [Fact]
//        public void when_export_mspec_with_nested_spec()
//        {
//            var mSpecWithNestedType = typeof(MSpecWithNested);
//            var specs = Utils.ExportSpecs();
//            Assert.True(specs.All(s => s.Description != typeof (MSpecWithNested.Nested1).Name));
//
//            var specContainer = (SpecContainer) ReadSpec(mSpecWithNestedType);
//            VerifySpecContainer(mSpecWithNestedType, specContainer);
//
//            var nested1Type = typeof(MSpecWithNested.Nested1);
//            var nested1SpecInfo = (SpecInfo)specContainer.Specs.First();
//            VerifySpecInfo(nested1Type, nested1SpecInfo, specContainer);
//
//            var nested2Type = typeof(MSpecWithNested.Nested2);
//            var nested2Container = (SpecContainer)specContainer.Specs.Last();
//            VerifySpecContainer(nested2Type, nested2Container);
//
//            var nested3Type = typeof(MSpecWithNested.Nested2.Nested3);
//            var nested3SpecInfo = (SpecInfo)nested2Container.Specs.First();
//            VerifySpecInfo(nested3Type, nested3SpecInfo, nested2Container);
//
//            var nested4Type = typeof(MSpecWithNested.Nested2.Nested4);
//            var nested4SpecInfo = (SpecInfo)nested2Container.Specs.Last();
//            VerifySpecInfo(nested4Type, nested4SpecInfo, nested2Container);
//        }
//
//        [Theory]
//        [InlineData(typeof(MSpec_Class_For_Search_In_Underline_NameSpace), "with underline namespace")]
//        [InlineData(typeof(MSpecClassForSearchInCamelNamespace), "with camel namespace")]
//        public void search_by_namespace(Type specType, string critiria)
//        {
//            var readSpecs = Utils.ExportSpecs();
//
//            var mSpecManager = new MSpecManager();
//            var specs = mSpecManager.Search(readSpecs, critiria);
//
//            Assert.Equal(1, specs.Count);
//            var spec = specs.Single();
//            Assert.Equal(specType.Name, spec.Description);
//        }
//        
//        [Theory]
//        [InlineData(typeof(MSpec_Class_For_Search_In_Underline_NameSpace), "class for search in underline")]
//        [InlineData(typeof(MSpecClassForSearchInCamelNamespace), "class for search in camel")]
//        public void search_by_class_name(Type specType, string critiria)
//        {
//            var readSpecs = Utils.ExportSpecs();
//
//            var mSpecManager = new MSpecManager();
//            var specs = mSpecManager.Search(readSpecs, critiria);
//
//            Assert.Equal(1, specs.Count);
//            var spec = specs.Single();
//            Assert.Equal(specType.Name, spec.Description);
//        }
//
//        [Fact]
//        public void export_tiger_mspec_to_txt_file()
//        {
//            var testFileName = "C:\\workspace\\Tiger\\Test\\Tiger-Core-Test\\bin\\Debug\\Tiger.API.Test.dll";
//
//            var mSpecManager = new MSpecManager();
//            IList<SpecBase> specs = mSpecManager.Export(testFileName);
//
//            Assert.True(specs.Count > 0);
//
//            MSpecTxtOutput.OutputToTxt(specs, "C:\\Users\\Administrator\\Desktop\\mspec.txt");
//        }
//
//        private static SpecBase ReadSpec(Type standardSimpleMSpecType)
//        {
//            var specs = Utils.ExportSpecs();
//
//            return specs.Single(s => s.Description == standardSimpleMSpecType.Name);
//        }
//
//        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
//        private static void VerifySpecInfo(Type standardMSpecType, SpecInfo specInfo, SpecContainer parent = null)
//        {
//            Assert.Equal(standardMSpecType.Namespace, specInfo.Namespace);
//
//            Assert.True(specInfo.Parent == parent);
//
//            VerifySpecElementInfos(standardMSpecType, typeof(Because), specInfo.Becauses);
//            VerifySpecElementInfos(standardMSpecType, typeof (It), specInfo.Its);
//        }
//
//        private static void VerifySpecElementInfos(Type standardMSpecType, Type specElementType, List<string> specElementInfos)
//        {
//            Assert.Equal(standardMSpecType.Fields().Count(f => f.Is(specElementType)), specElementInfos.Count);
//
//            var itFieldInfos = standardMSpecType.Fields(f => f.Is(specElementType));
//            for (int i = 0; i < specElementInfos.Count; i++)
//            {
//                Assert.Equal(itFieldInfos[i].Name, specElementInfos[i]);
//            }
//        }
//
//        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
//        private static void VerifySpecContainer(Type mSpecWithNestedType, SpecContainer specContainer)
//        {
//            Assert.Equal(mSpecWithNestedType.Namespace, specContainer.Namespace);
//            Assert.Equal(mSpecWithNestedType.Name, specContainer.Description);
//        }
    }
}
