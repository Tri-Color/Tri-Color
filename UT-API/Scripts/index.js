"use strict";

$(function(){
	var allUtData;

	loadAllTests();

	$("input").first().on("input", onSearchCriteriaChanged);
	// $("#searchButton").on("click", onSearch);
	// $("#listAllButton").on("click", loadAllTests);

	function loadAllTests(){
		$.get( "UnitTests", function( data ) {
			allUtData = data;
			appendTopUlToDocument(data["mspecInfos"], "API Tests");
			appendTopUlToDocument(data["xunitInfos"], "Unit Tests");
			appendTopUlToDocument(data["jtInfos"], "JavaScript Tests");
		});
	}

	function appendTopUlToDocument(utData, title){
		var utTitleElement = $("<h3/>").text(title);
		var bodyElement = $("body");
	  	bodyElement.append(utTitleElement);

		appendUtInfoElement(bodyElement, utData);
	}

	function appendUtInfoElement(parentElement, utInfos){
		var outerUlElement = $("<ul/>");
		parentElement.append(outerUlElement);

		_.each(utInfos, function(utInfo){
			var liElement = $("<li/>");
			outerUlElement.append(liElement);

			appendDivElement(liElement, utInfo.Description);

			appendArrayAsDivs(liElement, utInfo.WhenList);
			appendArrayAsDivs(liElement, utInfo.ThenList);

			var children = utInfo.Children;
			appendUtInfoElement(liElement, children);
		});
	}

	function appendArrayAsDivs(element, array){
		_.each(array, function(item){
			appendDivElement(element, item);
		})
	}

	function appendDivElement(element, text){
		var divElement = $("<div/>").text(text);
		element.append(divElement);
	}

	function onSearchCriteriaChanged(){
		var keyword = $(this).val().trim();
		var allUtLiElements = $("body ul li");

		for(var i = 0; i < allUtLiElements.length; i++){
			var liElement = allUtLiElements.eq(i);

			var allDivElements = liElement.find("div");
			for(var j = 0; j < allDivElements.length; j++){
				var divElement = allDivElements.eq(j);

				if(divElement.text().indexOf(keyword) > -1){
					liElement.css("display", "list-item");
				}
				else{
					liElement.css("display", "none");
				}
			}
		}
	}
});