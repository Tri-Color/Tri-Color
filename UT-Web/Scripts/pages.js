"use strict";

$(function(){
	var pages = UTWebUtils.pages = {
		"search-home": {
			template: "pages/search-home.htm",
			controller: "SearchHomeController"
		},
		"search-result": {
			template: "pages/search-result.htm",
			controller: "SearchResultController"
		}
	};
});