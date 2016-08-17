"use strict";

$(function(){
	utWebModule.register("SearchResultController", SearchResultController);
	
	function SearchResultController(param){
		search(param);
		$("#search-keyword").val(param);

		$("#search-button").on("click", onSearch);

		function search(param){
			var uri = "Search?query=" + param;

			$.get(uri, function( data ) {
				renderUtData(data);
			});
		}

		function onSearch(param){
			var searchKeyword = $("#search-keyword").val();
			search(searchKeyword);
		}

		function renderUtData(projectUtInfos){
			$("#test-tabs").remove();

			var testTabsElement = $("<div/>")
				.attr("id", "test-tabs")
				.attr("class", "search-result-tabs");

			$(".header").after(testTabsElement);
			
			var ulElement = $("<ul/>");
			testTabsElement.append(ulElement);

			var isSetShowTab = false;
			_.each(projectUtInfos, function(projectUtInfo){
				appendProjectToDocument(testTabsElement, ulElement, projectUtInfo);
			});

			$("#test-tabs").tabs();
			showTabWithTests(projectUtInfos);
		}

		function showTabWithTests(projectUtInfos){
			for(var i = 0; i < projectUtInfos.length; i++){
				var projectUtInfo = projectUtInfos[i];
				if(getTestCaseCountByProjects([projectUtInfo]) > 0){
					$("#test-tabs").tabs({
						active: i
					});
					break;
				}
			}
		}

		function appendProjectToDocument(parentElement, ulElement, projectUtInfo){
			var projectName = projectUtInfo.ProjectName;
			var tabTitle = projectName + "(" + getTestCaseCountByProjects([projectUtInfo]) + ")";

			var anchorElement = $("<a/>")
				.attr("href", "#" + getProjectDivId(projectName))
				.append(tabTitle);

			var projectTitleElement = $("<li/>")
				.append(anchorElement);

			ulElement.append(projectTitleElement);

			var projectElement = $("<div/>")
				.attr("id", getProjectDivId(projectName));

			appendCountElementByProject(projectElement, [projectUtInfo]);

			appendUtInfosElements(projectElement, projectUtInfo.ApiTests, "API Tests");
			appendUtInfosElements(projectElement, projectUtInfo.UnitTests, "Unit Tests");
			appendUtInfosElements(projectElement, projectUtInfo.JavaScriptTests, "JavaScript Tests");

			parentElement.append(projectElement);
		}

		function getProjectDivId(projectName){
			return projectName.replace(" ", "") + "Div";
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
			            	children: [],
			            	state : { "opened" : true },
	                    	icon : "glyphicon"
			            },
			        ]
			    }
			};
		  	generateTreeChildrenData(treeData.core.data[0], utData);
		  	var divElement = $("<div/>").attr("class", "tree");
		  	divElement.jstree(treeData);
		  	parentElement.append(divElement);
		}

		function generateTreeChildrenData(treeData, utDataArray){
			if (!utDataArray || utDataArray.length === 0) {
				return;
			}

			_.each(utDataArray, function(utInfo){
				var currentChild = {
					text: utInfo.Description,
					children: [],
	            	state : { "opened" : true },
	            	icon : "glyphicon"
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
						text: getContent(item),
						icon: "glyphicon"
					});
				})
			}
		}

		function appendCountElementByProject(parentElement, projectUtInfos){
			var testFileCount = getTestFileCountByProject(projectUtInfos);
			var testSuiteCount = getTestSuiteCountByProject(projectUtInfos);
			var testCaseCount = getTestCaseCountByProjects(projectUtInfos);

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

		function getTestCaseCountByProjects(projectUtInfos){
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
			return utInfo.ThenList.length + _.sumBy(utInfo.Children, getTestCaseCountByUtInfo);
		}
	}
});