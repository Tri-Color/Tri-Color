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
		$.get( "UnitTests", function( data ) {
			renderUtData(data);
		});
	}

	function renderUtData(projectUtInfos){
		var testListElement = $("#testList");
		testListElement.children().empty();

		_.each(projectUtInfos, function(projectUtInfo){
			appendProjectToDocument(testListElement, projectUtInfo);
		});
	}

	function appendProjectToDocument(parentElement, projectUtInfo){
		var projectTitleElement = $("<h2/>").text(projectUtInfo.ProjectName);
		parentElement.append(projectTitleElement);

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
});