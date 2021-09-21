using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Service;
using SortAlgorithm.Controllers;
using SortAlgorithm.Models;
using System;
using FluentAssertions;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain;

namespace Tests
{
    [TestClass]
    public class SortControllerTests
    {
        private ILogger<SortController> _logger;
        private IServiceProvider _serviceProvider;
        private IFileOperation _fileIO;
        private IHostEnvironment _hostEnvironment;
        private IOptions<AppSettings> _appSettings;
        private IAlgorithmFactory _algorithmFactory;

        [TestInitialize]
        public void Initialize()
        {
            _logger = Substitute.For<ILogger<SortController>>();
            _serviceProvider = Substitute.For<IServiceProvider>();
            _fileIO = Substitute.For<IFileOperation>();
            _hostEnvironment = Substitute.For<IHostEnvironment>();
            _appSettings = Options.Create(new AppSettings { ResultFileName = "ResultSummary.txt" });
            _algorithmFactory = Substitute.For<IAlgorithmFactory>();
        }

        [TestMethod]
        public  void WhenSortTypeIsNotPassed_ThenRaiseCorrespondingError()
        {
            //Arrange
            var controllerUnderTest = new SortController(_logger,_serviceProvider,_fileIO,_hostEnvironment,_appSettings,_algorithmFactory);
            var unsortedIntegers = new int[] { 10, 5, 54, 24, 19, 45, 99, 78, 65, 11, 41, 87 };

            //Act
            var response = controllerUnderTest.SortInput(unsortedIntegers, "") as ObjectResult;

            //Assert
            response.Should().BeOfType(typeof(BadRequestObjectResult));
            response.StatusCode.Should().Be(400);
            response.Value.Should().Be("Invalid Input, Please check the input passed");
        }

        [TestMethod]
        public void GivenAllRequiredInputIsReceived_WhenFileCreationErrors_ThenRaiseCorrespondingError()
        {
            //Arrange
            var unsortedIntegers = new int[] { 10, 5, 54, 24, 19, 45, 99, 78, 65, 11, 41, 87 };
            var sortAlgorithm = Substitute.For<ISortAlgorithm>();

            sortAlgorithm.DoSort(unsortedIntegers).Returns(new int[] { });
            _algorithmFactory.GetSortAlgorithm(Arg.Any<string>()).Returns(sortAlgorithm);
            _fileIO.Write(Arg.Any<SortResultSummary>()).Returns(false);

            var controllerUnderTest = new SortController(_logger, _serviceProvider, _fileIO, _hostEnvironment, _appSettings, _algorithmFactory);

            //Act
            var response = controllerUnderTest.SortInput(unsortedIntegers, "Bubblesort") as ObjectResult;

            //Assert
            response.Should().BeOfType(typeof(BadRequestObjectResult));
            response.StatusCode.Should().Be(400);
            response.Value.Should().Be("An error occured, File was not created successfully");
        }

        [TestMethod]
        public void GivenAllRequiredInputIsReceived_WhenSortTypeAlgorithmIsInvalid_ThenRaiseCorrespondingError()
        {
            //Arrange
            var unsortedIntegers = new int[] { 10, 5, 54, 24, 19, 45, 99, 78, 65, 11, 41, 87 };
            _algorithmFactory.GetSortAlgorithm(Arg.Any<string>()).Returns(i => null);
            _fileIO.Write(Arg.Any<SortResultSummary>()).Returns(false);

            var controllerUnderTest = new SortController(_logger, _serviceProvider, _fileIO, _hostEnvironment, _appSettings, _algorithmFactory);

            //Act
            var response = controllerUnderTest.SortInput(unsortedIntegers, "SomeSortType") as ObjectResult;

            //Assert
            response.Should().BeOfType(typeof(BadRequestObjectResult));
            response.StatusCode.Should().Be(400);
            response.Value.Should().Be("Entered algorithm type is not supported, Please enter `Bubblesort` or `Mergesort`");
        }
    }
}
