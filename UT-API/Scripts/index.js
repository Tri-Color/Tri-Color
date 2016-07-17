"use strict";

$(function(){
	$("#searchButton").on("click", onSearch);
	$("#listAllButton").on("click", loadAllTests);

	function onSearch(){
		var searchKeyword = $("#searchKeyword").val();
		// var uri = "Search?query={0}".format(searchKeyword);
		console.log($("#searchKeyword"))
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

	function renderUtData(data){
		var testListElement = $("#testList");
		testListElement.children().empty();

		appendTopUlToDocument(testListElement, data["mspecInfos"], "API Tests");
		appendTopUlToDocument(testListElement, data["xunitInfos"], "Unit Tests");
		appendTopUlToDocument(testListElement, data["jtInfos"], "JavaScript Tests");
	}

	function appendTopUlToDocument(parentElement, utData, title){
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