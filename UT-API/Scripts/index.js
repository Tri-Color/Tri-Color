"use strict";

$(function(){
	$("#searchButton").on("click", onSearch);
	$("#listAllButton").on("click", loadAllTests);

	function onSearch(){
		var searchKeyword = $("#searchKeyword").val();
		var uri = "Search?query=" + searchKeyword;

		$.get(uri, function( data ) {
			renderUtData(data);
		});
	}

	function loadAllTests(){
		$("#searchKeyword").val("");

		$.get( "UnitTests", function( data ) {
			renderUtData(data);
		});
	}

	function renderUtData(projectUtInfos){
		var testListElement = $("#testList");
		testListElement.children().empty();

		appendCountElementByProject(testListElement, projectUtInfos);

		_.each(projectUtInfos, function(projectUtInfo){
			appendProjectToDocument(testListElement, projectUtInfo);
		});
	}

	function appendProjectToDocument(parentElement, projectUtInfo){
		var projectTitleElement = $("<h2/>").text(projectUtInfo.ProjectName);
		parentElement.append(projectTitleElement);

		appendCountElementByProject(parentElement, [projectUtInfo]);

		appendUtInfosElements(parentElement, projectUtInfo.ApiTests, "API Tests");
		appendUtInfosElements(parentElement, projectUtInfo.UnitTests, "Unit Tests");
		appendUtInfosElements(parentElement, projectUtInfo.JavaScriptTests, "JavaScript Tests");
	}

	function appendUtInfosElements(parentElement, utData, title){
		if(!utData || utData.length <= 0){
			return;
		}

		var utTitleElement = $("<h3/>").text(title);
	  	parentElement.append(utTitleElement);

	  	appendCountElementByUtInfos(parentElement, utData);

		appendUtInfoElement(parentElement, utData);
	}

	function appendUtInfoElement(parentElement, utInfos){
		var outerUlElement = $("<ul/>");
		parentElement.append(outerUlElement);

		_.each(utInfos, function(utInfo){
			var liElement = $("<li/>");
			outerUlElement.append(liElement);

			appendDivElement(liElement, utInfo.Description);

			appendWhenListAsDivs(liElement, utInfo.WhenList);
			appendThenListAsDivs(liElement, utInfo.ThenList);

			var children = utInfo.Children;
			appendUtInfoElement(liElement, children);
		});
	}

	function appendWhenListAsDivs(element, array){
		_.each(array, function(item){
			appendDivElement(element, item);
		})
	}

	function appendThenListAsDivs(element, array){
		_.each(array, function(item){
			var text = !!item.IsParameterized ? 
				"[Parameterized] " + item.Description:
				item.Description
			appendDivElement(element, text);
		})
	}

	function appendDivElement(element, text){
		var divElement = $("<div/>").text(text);
		element.append(divElement);
	}

	function appendCountElementByProject(parentElement, projectUtInfos){
		var testFileCount = getTestFileCountByProject(projectUtInfos);
		var testSuiteCount = getTestSuiteCountByProject(projectUtInfos);
		var testCaseCount = getTestCaseCountByProject(projectUtInfos);

		var text = "Test File: " + testFileCount + "; " +
					"Test Suite: " + testSuiteCount + "; " +
					"Test Case: " + testCaseCount + ".";
		var countElement = $("<p/>").text(text);
		parentElement.append(countElement);
	}

	function appendCountElementByUtInfos(parentElement, utInfos){
		var testFileCount = getTestFileCountByUtInfos(utInfos);
		var testSuiteCount = getTestSuiteCountByUtInfos(utInfos);
		var testCaseCount = getTestCaseCountByUtInfos(utInfos);

		var text = "Test File: " + testFileCount + "; " +
					"Test Suite: " + testSuiteCount + "; " +
					"Test Case: " + testCaseCount + ".";
		var countElement = $("<p/>").text(text);
		parentElement.append(countElement);
	}

	function getTestFileCountByProject(projectUtInfos){
		return _.sumBy(projectUtInfos, function(projectUtInfo){
			return getTestFileCountByUtInfos(projectUtInfo.ApiTests) +
				getTestFileCountByUtInfos(projectUtInfo.UnitTests) + 
				getTestFileCountByUtInfos(projectUtInfo.JavaScriptTests);
		});
	}

	function getTestFileCountByUtInfos(utInfos){
		return utInfos.length;
	}

	function getTestSuiteCountByProject(projectUtInfos){
		return getCountByProjectRecursively(projectUtInfos, getTestSuiteCountByUtInfo);
	}

	function getTestSuiteCountByUtInfos(utInfos){
		return _.sumBy(utInfos, getTestSuiteCountByUtInfo);
	}

	function getTestSuiteCountByUtInfo(utInfo){
		var childrenCount = _.sumBy(utInfo.Children, getTestSuiteCountByUtInfo);
		return childrenCount > 1 ? childrenCount : 1;
	}

	function getTestCaseCountByProject(projectUtInfos){
		return getCountByProjectRecursively(projectUtInfos, getTestCaseCountByUtInfo);
	}

	function getTestCaseCountByUtInfos(utInfos){
		return _.sumBy(utInfos, getTestCaseCountByUtInfo);
	}

	function getCountByProjectRecursively(projectUtInfos, getCountByUtInfo){
		return _.sumBy(projectUtInfos, function(projectUtInfo){
			return _.sumBy(projectUtInfo.ApiTests, getCountByUtInfo) +
				_.sumBy(projectUtInfo.UnitTests, getCountByUtInfo) + 
				_.sumBy(projectUtInfo.JavaScriptTests, getCountByUtInfo);
		});
	}

	function getTestCaseCountByUtInfo(utInfo){
		return utInfo.ThenList.length + _.sumBy(utInfo.Children, getTestSuiteCountByUtInfo);
	}
});