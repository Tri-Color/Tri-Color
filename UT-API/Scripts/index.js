"use strict";

$(function(){
	$.get( "UnitTests", function( data ) {
	  appendTopUlToDocument(data["mspecInfos"], "MSpec");
	  appendTopUlToDocument(data["xunitInfos"], "XUnit");
	  appendTopUlToDocument(data["jtInfos"], "JT");
	});

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
});