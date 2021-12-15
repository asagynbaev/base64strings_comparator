using NUnit.Framework;
using RadioFreeEurope.Models;
using RadioFreeEurope.Services;

namespace RadioFreeEuropeTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestInputsAreEqual()
        {
            DiffService diffService = new();

            Diff left = new() { Id = 1, Base64Value = "string", Type = DiffType.Left };
            Diff right = new() { Id = 1, Base64Value = "string", Type = DiffType.Right };
            
            string expected = "inputs are equal";
            var actual = diffService.Compare(left, right);

            Assert.AreEqual(expected, actual.Message);
        }

        [Test]
        public void TestInputsAreOfDifferentSize()
        {
            DiffService diffService = new();

            Diff left = new() { Id = 1, Base64Value = "string", Type = DiffType.Left };
            Diff right = new() { Id = 1, Base64Value = "strimg123", Type = DiffType.Right };

            string expected = "inputs are of different size";
            var actual = diffService.Compare(left, right);

            Assert.AreEqual(expected, actual.Message);
        }

        [Test]
        public void TestInputsWithSameSize()
        {
            DiffService diffService = new();

            Diff left = new() { Id = 1, Base64Value = "string", Type = DiffType.Left };
            Diff right = new() { Id = 1, Base64Value = "ztrimg", Type = DiffType.Right };

            string expected = "input with diffs";
            var actual = diffService.Compare(left, right);

            Assert.AreEqual(expected, actual.Message);
        }
    }
}