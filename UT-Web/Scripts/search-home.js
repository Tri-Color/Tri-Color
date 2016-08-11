"use strict";

$(function(){
	utWebModule.register("SearchHomeController", SearchHomeController);

	function SearchHomeController(){
		$("#search-button").on("click", onSearch);

		function onSearch(){
			var searchKeyword = $("#search-keyword").val();
			UTWebUtils.goto("search-result", searchKeyword);
		}
	}
});