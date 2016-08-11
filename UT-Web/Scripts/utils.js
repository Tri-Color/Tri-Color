"use strict";

$(function(){

	var UTWebUtils = window.UTWebUtils = {};
	UTWebUtils.goto = goto;

	var utWebModule = window.utWebModule = {};
	utWebModule.register = register;

	var _currentController;

	function goto(pageName, param){
		var page = UTWebUtils.pages[pageName];
		
		$("div#content").load(page.template, function(){
			_currentController = new utWebModule[page.controller](param);
		});
	}

	function register(controllerName, controllerFunc){
		this[controllerName] = controllerFunc;
	}
});