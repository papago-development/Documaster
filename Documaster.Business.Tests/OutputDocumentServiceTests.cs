using System;
using System.Collections.Generic;
using Documaster.Business.Services;
using Documaster.Model.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Documaster.Business.Tests
{
    [TestClass]
    public class OutputDocumentServiceTests
    {
        private static IGenericRepository<OutputDocument> _outputDocumentRepository;
        private static IUnitOfWork _unitOfWork;
        private static OutputDocument outputDocument;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _outputDocumentRepository = MockRepository.GenerateMock<IGenericRepository<OutputDocument>>();
            _unitOfWork = MockRepository.GenerateMock<IUnitOfWork>();
        }
        
        [TestInitialize]
        public void TestInit()
        {
            _outputDocumentRepository.Stub(m => m.Update(outputDocument, new List<string>())).Return(true);
            _outputDocumentRepository.Stub(m => m.Create(outputDocument)).Return(outputDocument);

        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
