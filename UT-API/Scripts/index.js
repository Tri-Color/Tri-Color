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
		testListElement.children().remove();
		var projectFilterDivElement = $("#projectFilterSpan");
		projectFilterDivElement.children().remove();

		appendCountElementByProject(testListElement, projectUtInfos);

		var headerElement = $("#header");
		_.each(projectUtInfos, function(projectUtInfo){
			appendCheckbox(projectFilterDivElement, projectUtInfo.ProjectName);
			appendProjectToDocument(testListElement, projectUtInfo);
		});
	}

	function appendCheckbox(parentElement, projectName){
		var checkboxElement = $("<input/>")
			.attr("id", projectName)
			.attr("type", "checkbox")
			.attr("checked", true)
			.change(onCheckboxChanged);
		var textElement = $("<span/>").text(projectName);
		var wrapperElement = $("<span/>")
			.append(checkboxElement)
			.append(textElement);
		parentElement.append(wrapperElement);
	}

	function onCheckboxChanged(){
		var projectName = $(this).attr("id");
		var projectDivId = getProjectDivId(projectName);
		var displayValue = $(this).is(":checked") ? "block" : "none";
		$("#" + projectDivId).css("display", displayValue);
	}

	function appendProjectToDocument(parentElement, projectUtInfo){
		var projectName = projectUtInfo.ProjectName;
		var projectTitleElement = $("<h2/>").text(projectName);

		var projectElement = $("<div/>")
			.attr("id", getProjectDivId(projectName))
			.append(projectTitleElement);

		appendCountElementByProject(projectElement, [projectUtInfo]);

		appendUtInfosElements(projectElement, projectUtInfo.ApiTests, "API Tests");
		appendUtInfosElements(projectElement, projectUtInfo.UnitTests, "Unit Tests");
		appendUtInfosElements(projectElement, projectUtInfo.JavaScriptTests, "JavaScript Tests");

		parentElement.append(projectElement);
	}

	function getProjectDivId(projectName){
		return projectName + "Div";
	}

	function appendUtInfosElements(parentElement, utData, title){
		if(!utData || utData.length <= 0){
			return;
		}

		var utTitleElement = $("<h3/>").text(title);
	  	parentElement.append(utTitleElement);

	  	appendCountElementByUtInfos(parentElement, utData);

	  	var treeData = {
		    'core' : {
		        'data' : [
		            { 
		            	"text" : title,
		            	children: []
		            },
		        ]
		    }
		};
	  	generateTreeChildrenData(treeData.core.data[0], utData);
	  	var divElement = $("<div/>");
	  	divElement.jstree(treeData);
	  	console.log(treeData)
	  	parentElement.append(divElement);
		// var topUlElement = appendUtInfoElement(parentElement, utData);
		// $("ul").jstree();
		// topUlElement.jstree();
	}

	function generateTreeChildrenData(treeData, utDataArray){
		if (!utDataArray || utDataArray.length === 0) {
			return;
		}

		_.each(utDataArray, function(utInfo){
			var currentChild = {
				text: utInfo.Description,
				children: []
			};

			AddUtDetailToTreeData(currentChild, utInfo.WhenList, function(item){
				return item;
			});
			AddUtDetailToTreeData(currentChild, utInfo.ThenList, function(item){
				var prefix = !!item.IsParameterized ? "[Parameterized] " : "";
				return prefix + item.Description;
			});
			generateTreeChildrenData(currentChild, utInfo.Children);

			treeData.children.push(currentChild);
		});
	}

	function AddUtDetailToTreeData(treeData, detailList, getContent){
		if(!!detailList && detailList.length > 0){
			_.each(detailList, function(item){
				treeData.children.push({
					text: getContent(item)
				});
			})
		}
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