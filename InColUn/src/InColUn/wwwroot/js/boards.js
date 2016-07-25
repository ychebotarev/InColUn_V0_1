/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId])
/******/ 			return installedModules[moduleId].exports;
/******/
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			exports: {},
/******/ 			id: moduleId,
/******/ 			loaded: false
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.loaded = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(0);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var App_1 = __webpack_require__(1);
	App_1.application.SetRoot(document.getElementById('app'));
	App_1.application.Render();
	App_1.application.Initialize();


/***/ },
/* 1 */
/***/ function(module, exports, __webpack_require__) {

	/// <reference path='../typings/index.d.ts' />
	"use strict";
	var SideBar_1 = __webpack_require__(2);
	var ContentArea_1 = __webpack_require__(7);
	var App = (function () {
	    function App() {
	        var _this = this;
	        this.commands = {};
	        this.sidebar = new SideBar_1.SideBar();
	        this.contentArea = new ContentArea_1.ContentArea();
	        var nodes = [
	            {
	                info: { title: 'Misc Information', guid: '57be4c62-5ca1-4800-974f-11b7e92eda37' },
	                subNodes: [
	                    {
	                        info: { title: 'Statistic lactures', guid: '1d010db1-0980-4708-8c57-24d640717998' },
	                        subNodes: [
	                            { info: { title: 'Lecture 1', guid: '74253706-fdef-4acc-9bd5-15c3ae7e5bf7' } },
	                            { info: { title: 'Definition', guid: 'ece1cfeb-cac2-477d-94d9-5e880d21ea2d' } },
	                            { info: { title: 'Lecture 2', guid: 'b0e7cd43-e9d6-4f20-9c40-da4b30947db3' } },
	                            { info: { title: 'Definition', guid: 'c30ed5f5-075a-4e63-a4c3-8df587eb5d1c' } },
	                            { info: { title: 'Type of variables', guid: '83301322-95e8-41ac-ac4c-eac7a121a028' } },
	                            { info: { title: 'Correlation and measure', guid: 'a93f94b5-27a0-4abb-85bc-16d0a397e86d' } }
	                        ]
	                    },
	                    {
	                        info: { title: 'Film and books', guid: 'ee69e02c-9663-4a96-b6f9-3d289246f07d' },
	                        subNodes: [
	                            { info: { title: 'Books to read', guid: 'ae5d3e4b-191e-4da8-9cae-cf73731747a9' } },
	                            { info: { title: 'Favourite book', guid: 'db6c23d8-1408-493b-b4bd-74ff4e865edb' } },
	                            { info: { title: 'Movies to watch', guid: '15ceda6a-2b36-45b4-a13e-87edf0ebd2cb' } }
	                        ]
	                    },
	                    {
	                        info: { title: 'Ukraine', guid: '0cdc9c6c-d7d9-41c2-9c7d-4666fb702b37' },
	                        subNodes: [
	                            { info: { title: 'Odessa, what happen on may 2', guid: '5c47797a-e4b2-4fbe-ba9f-23a4989c10ae' } },
	                            { info: { title: 'Ukrainian nationalists', guid: 'c932280f-55d3-454f-b47e-9625d0e5f16d' } },
	                            { info: { title: 'Civil war at Ukraine', guid: '2de1a32b-db8d-42a8-a38d-4a0ce00560e5' } },
	                            { info: { title: 'Референдум', guid: '9e77cf29-538b-4a8b-8f2a-9b21c572b9d8' } },
	                            { info: { title: 'Знаковые видео', guid: 'd850eff7-16f2-4ae3-bba3-da88b7d54f32' } },
	                            { info: { title: 'Фейки украинских СМИ', guid: '295235b3-19fe-4569-93e2-0983bc4c99e8' } },
	                            { info: { title: 'Разное', guid: 'f19f4923-01a4-4687-9b86-3d71215717f5' } },
	                            { info: { title: 'Порошенко', guid: '839274a8-2aa6-4b55-a204-c23357119076' } },
	                            { info: { title: 'ВСУ', guid: 'e722f740-60d0-4046-b54c-fe2071305784' } }
	                        ]
	                    },
	                    {
	                        info: { title: 'Interview questions', guid: '2f3d4139-adad-42ec-8c99-f7a820eab9a9' },
	                        subNodes: [
	                            { info: { title: 'Google interview questions', guid: 'd5220776-cd55-4eb4-8b31-97eaecbdda74' } },
	                            { info: { title: 'More standard questions info', guid: '64efb990-09da-43a3-ae29-6aca1db6e20e' } },
	                            { info: { title: 'Истории для интервью', guid: 'a4c7f3ba-728e-435a-bde9-10d965947b5b' } },
	                            { info: { title: 'Топики для интервью', guid: 'dfb066ed-5b04-4b06-a500-4de42fd4cc27' } },
	                            { info: { title: 'Multithreading', guid: '024c0c57-2689-4191-a31e-445b321c9675' } },
	                            { info: { title: 'Not job related questions', guid: '86660a4e-278e-40a1-b274-e93b661583ce' } },
	                        ]
	                    },
	                    { info: { title: 'Food questions', guid: '2e3b69d6-aa36-4d6a-98d2-d2e48769a446' } },
	                    {
	                        info: { title: 'Job search', guid: 'aadfc7ee-96ba-40ca-a2c8-5c513c81776b' },
	                        subNodes: [
	                            { info: { title: 'Current process', guid: 'c8d2e0da-97d0-4a7d-8481-23703591c15a' } },
	                            { info: { title: 'Important algorithm', guid: '679ed614-9ed2-460d-9689-2b6d56b6f47f' } },
	                            { info: { title: 'c++ interview questions', guid: '706e1339-bde1-4dca-8fc8-262d0d90fbe7' } }
	                        ]
	                    },
	                    { info: { title: 'Training - Jym', guid: '93652d5c-f467-4f42-ab1b-ee792b638a20' } },
	                    { info: { title: 'Team Leading', guid: 'ae8ce805-cacf-47f0-a4b5-9757b0df3c47' } },
	                ]
	            },
	            {
	                info: { title: 'GTD', guid: '2af35ed7-8479-4b89-87eb-82353d3601b5' },
	                subNodes: [
	                    {
	                        info: { title: 'Tasks', guid: '4e665ff3-5603-4321-b46e-53f64901d03a' },
	                        subNodes: [
	                            { info: { title: 'child_level3_a', guid: '162dc256-f289-4b92-a9e6-682ce6c88545' } },
	                            { info: { title: 'child_level3_b', guid: 'ab38fe3d-8a48-4ac6-b71f-71c924116269' } }
	                        ]
	                    },
	                    { info: { title: 'Weekly review', guid: 'f97e0251-3765-46c6-8b8a-0bd331c10db2' } },
	                    { info: { title: 'Backlog', guid: 'e5c88949-025a-4d62-b694-f3f4d64802d8' } },
	                    { info: { title: 'Project List', guid: '5253a0a6-cf8b-4aa9-82dc-f9cff3bc0c1c' } }
	                ]
	            },
	            {
	                info: { title: 'Recycled Boards', guid: '50849231-ec86-44ab-93bc-61f197e240d5', commandInfo: { command: 'OpenRecycledBoards' } }
	            }
	        ];
	        this.sidebar.LoadTreeContainer(nodes);
	        var openPageCallback = function (param1, param2) { _this.contentArea.OnOpenPage(param1['guid']); };
	        this.SetCommandDispatcher('OpenBoard', function (param1, param2) { _this.OnOpenBoard(param1['guid']); });
	        this.SetCommandDispatcher('OpenPage', openPageCallback);
	        this.SetCommandDispatcher('OpenBoards', function () { _this.contentArea.OnOpenBoards(); });
	        this.SetCommandDispatcher('OpenSavedBoards', function () { _this.contentArea.OnOpenSavedBoards(); });
	        this.SetCommandDispatcher('OpenRecycledBoards', function () { _this.contentArea.OnOpenRecycledBoards(); });
	        this.SetCommandDispatcher('HideSideBar', function () { _this.sidebar.HideSideBar(); });
	    }
	    App.prototype.SetCommandDispatcher = function (key, command) {
	        this.commands[key] = command;
	    };
	    App.prototype.RemoveCommandDispatcher = function (key) {
	        this.commands[key] = undefined;
	    };
	    App.prototype.OnCommand = function (commandInfo) {
	        var dispatch = this.commands[commandInfo.command];
	        if (dispatch === undefined) {
	            console.log("Can't find handle for command: " + commandInfo.command);
	        }
	        else {
	            dispatch(commandInfo.param1, commandInfo.param2);
	        }
	    };
	    App.prototype.SetRoot = function (root) {
	        this.root = root;
	        var content_area = document.getElementById('content_area');
	        this.contentArea.SetRoot(content_area);
	        var sidebar = document.getElementById('sidebar_menu');
	        this.sidebar.SetRoot(sidebar);
	    };
	    App.prototype.LoadBoardInfo = function (user) {
	    };
	    App.prototype.AddBoard = function (board) {
	        this.boards[board.getID()] = board;
	    };
	    App.prototype.Render = function () {
	        var sidebar = document.getElementById('sidebar_menu');
	        if (sidebar != undefined) {
	            this.sidebar.Render();
	        }
	        else {
	            console.log("can't find sidebar");
	        }
	        var content_area = document.getElementById('content_area');
	        if (content_area != undefined) {
	            this.contentArea.Render();
	        }
	        else {
	            console.log("can't find content_area");
	        }
	    };
	    App.prototype.Initialize = function () {
	        this.sidebar.OnLoadRecentBoards();
	        this.contentArea.OnOpenBoards();
	    };
	    App.prototype.OnOpenBoard = function (id) {
	        var _this = this;
	        $.ajax('/api/v1.0/board/' + id, {
	            type: 'GET',
	            data: {},
	            dataType: 'json',
	            success: function (data, textStatus, jqXHR) { _this.OnBoardLoaded(data, textStatus, jqXHR); }
	        });
	    };
	    App.prototype.OnBoardLoaded = function (data, textStatus, jqXHR) {
	        if (!data || data.success != true) {
	            return;
	        }
	        var sections = data.sections;
	        var nodes = this.SectionsToNodes(sections);
	        //this.sidebar.LoadTreeContainer(nodes);
	    };
	    App.prototype.SectionsToNodes = function (sections) {
	        return null;
	    };
	    return App;
	}());
	var application = new App();
	exports.application = application;


/***/ },
/* 2 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var TreeContainer_1 = __webpack_require__(3);
	var SideBar = (function () {
	    function SideBar() {
	    }
	    SideBar.prototype.LoadTreeContainer = function (nodes) {
	        this.treeContainer = new TreeContainer_1.TreeContainer(nodes);
	    };
	    SideBar.prototype.AddTreeNode = function (node) {
	        this.treeContainer.AddNode(node);
	    };
	    SideBar.prototype.SetRoot = function (sidebar) {
	        this.htmlElement = sidebar;
	    };
	    SideBar.prototype.Render = function () {
	        this.treeContainer.Render(this.htmlElement);
	    };
	    SideBar.prototype.OnLoadRecentBoards = function () {
	        var _this = this;
	        this.treeContainer.setLoading(true);
	        $.ajax('/api/v1.0/recent', {
	            type: 'GET',
	            data: {},
	            dataType: 'json',
	            success: function (data, textStatus, jqXHR) { _this.OnRecentLoaded(data, textStatus, jqXHR); }
	        });
	    };
	    SideBar.prototype.ShowSideBar = function () {
	        this.htmlElement.style.width = '350px';
	    };
	    SideBar.prototype.HideSideBar = function () {
	        this.htmlElement.style.width = '0px';
	    };
	    SideBar.prototype.OnRecentLoaded = function (data, textStatus, jqXHR) {
	        this.treeContainer.setLoading(false);
	        if (data.recent && data.recent.length > 0) {
	        }
	    };
	    return SideBar;
	}());
	exports.SideBar = SideBar;


/***/ },
/* 3 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __extends = (this && this.__extends) || function (d, b) {
	    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
	    function __() { this.constructor = d; }
	    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
	};
	var dom_1 = __webpack_require__(4);
	var UIElement_1 = __webpack_require__(5);
	var TreeNode_1 = __webpack_require__(6);
	var TreeContainer = (function (_super) {
	    __extends(TreeContainer, _super);
	    function TreeContainer(nodes) {
	        var _this = this;
	        _super.call(this);
	        this.isLoading = false;
	        this.nodes = [];
	        nodes.forEach(function (node) {
	            _this.nodes.push(new TreeNode_1.TreeNode(node, 0));
	        });
	    }
	    TreeContainer.prototype.setLoading = function (loading) {
	        this.isLoading = loading;
	    };
	    TreeContainer.prototype.AddNode = function (node) {
	        this.nodes.push(new TreeNode_1.TreeNode(node, 0));
	    };
	    TreeContainer.prototype.CreateDomImpl = function () {
	        return dom_1.Dom.div('treecontainer');
	    };
	    TreeContainer.prototype.RenderSelf = function () {
	        var treelist = dom_1.Dom.div('treelist');
	        this.nodes.forEach(function (node) {
	            node.Render(treelist);
	        });
	        this.htmlElement.appendChild(treelist);
	    };
	    return TreeContainer;
	}(UIElement_1.UIElement));
	exports.TreeContainer = TreeContainer;


/***/ },
/* 4 */
/***/ function(module, exports) {

	"use strict";
	var Dom = (function () {
	    function Dom() {
	    }
	    Dom.CreateFromProps = function (props) {
	        return Dom.Create(props.tag, props.className, props.style, props.attributes);
	    };
	    Dom.Create = function (tag, className, style, attributes) {
	        var element = document.createElement(tag);
	        if (className) {
	            element.className = className;
	        }
	        if (attributes) {
	            for (var attrName in attributes) {
	                if (attrName == 'style') {
	                    var style_attr = attributes['style'];
	                    for (var styleName in style_attr) {
	                        element.style[styleName] = style_attr[styleName];
	                    }
	                }
	                else {
	                    element.setAttribute(attrName, attributes[attrName]);
	                }
	            }
	        }
	        if (style) {
	            for (var styleName in style) {
	                element.style[styleName] = style[styleName];
	            }
	        }
	        return element;
	    };
	    Dom.element = function (tag, className, style, attributes) {
	        return Dom.Create(tag, className, style, attributes);
	    };
	    Dom.a = function (className, style, attributes) {
	        return Dom.Create('a', className, style, attributes);
	    };
	    Dom.div = function (className, style, attributes) {
	        return Dom.Create('div', className, style, attributes);
	    };
	    Dom.span = function (className, style, attributes) {
	        return Dom.Create('span', className, style, attributes);
	    };
	    Dom.ul = function (className, style, attributes) {
	        return Dom.Create('ul', className, style, attributes);
	    };
	    Dom.li = function (className, style, attributes) {
	        return Dom.Create('li', className, style, attributes);
	    };
	    Dom.img = function (path, className) {
	        return Dom.Create('img', className, {}, { src: path });
	    };
	    Dom.text = function (content, tag, className, style, attributes) {
	        var el = Dom.Create(tag, className, style, attributes);
	        el.innerText = content;
	        return el;
	    };
	    Dom.bootstrap_icon = function (icon) {
	        var cn = 'glyphicon glyphicon-' + icon;
	        return Dom.Create('i', cn);
	    };
	    Dom.GetElementByClassName = function (parent, className) {
	        if (!parent) {
	            return undefined;
	        }
	        var elements = parent.getElementsByClassName(className);
	        if (!elements || elements.length == 0) {
	            return undefined;
	        }
	        return elements[0];
	    };
	    Dom.Hide = function (element) {
	        element.style.display = 'none';
	    };
	    Dom.Show = function (element) {
	        element.style.display = 'block';
	    };
	    return Dom;
	}());
	exports.Dom = Dom;


/***/ },
/* 5 */
/***/ function(module, exports) {

	"use strict";
	var UIElement = (function () {
	    function UIElement() {
	    }
	    UIElement.prototype.CreateDom = function () {
	        this.htmlElement = this.CreateDomImpl();
	    };
	    UIElement.prototype.Render = function (renderTo) {
	        this.CreateDom();
	        if (!this.htmlElement) {
	            return undefined;
	        }
	        this.RenderSelf();
	        if (renderTo) {
	            renderTo.appendChild(this.htmlElement);
	        }
	    };
	    UIElement.prototype.Hide = function () {
	        if (!this.htmlElement) {
	            return;
	        }
	        this.htmlElement.style.display = 'none';
	    };
	    UIElement.prototype.Show = function (show_style) {
	        if (!this.htmlElement) {
	            return;
	        }
	        this.htmlElement.style.display = (!show_style) ? 'block' : show_style;
	    };
	    return UIElement;
	}());
	exports.UIElement = UIElement;


/***/ },
/* 6 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __extends = (this && this.__extends) || function (d, b) {
	    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
	    function __() { this.constructor = d; }
	    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
	};
	var dom_1 = __webpack_require__(4);
	var UIElement_1 = __webpack_require__(5);
	var App_1 = __webpack_require__(1);
	var TreeNode = (function (_super) {
	    __extends(TreeNode, _super);
	    function TreeNode(nodeInfo, level) {
	        var _this = this;
	        _super.call(this);
	        this.info = nodeInfo.info;
	        this.level = level;
	        this.nodes = [];
	        this.open = true;
	        if (nodeInfo.subNodes) {
	            nodeInfo.subNodes.forEach(function (node) {
	                _this.nodes.push(new TreeNode(node, level + 1));
	            });
	        }
	    }
	    TreeNode.prototype.ToggleSubTree = function () {
	        var treenode = document.getElementById('tn_' + this.info.guid);
	        if (!treenode) {
	            return;
	        }
	        var toggle_icon = (treenode.getElementsByClassName('tree-toggle-icon')[0]);
	        var treelist = (treenode.getElementsByClassName('treelist')[0]);
	        if (!toggle_icon || !treelist) {
	            console.log('error getting childs for id:' + this.info.guid);
	            return;
	        }
	        this.open = !this.open;
	        treelist.style.display = this.open ? 'block' : 'none';
	        toggle_icon.className = 'tree-toggle-icon fa fa fa-stack-1x fa-chevron-' + (this.open ? 'up' : 'down');
	        treelist.style.display = this.open ? 'block' : 'none';
	    };
	    TreeNode.prototype.CreateDomImpl = function () {
	        var node = dom_1.Dom.Create('div', 'treenode');
	        node.id = 'tn_' + this.info.guid;
	        return node;
	    };
	    TreeNode.prototype.RenderSelf = function () {
	        var header = this.RenderHeader();
	        var childs = this.RenderChilds();
	        this.htmlElement.appendChild(header);
	        if (childs != undefined) {
	            this.htmlElement.appendChild(childs);
	        }
	    };
	    TreeNode.prototype.getIconClass = function () {
	        if (this.nodes && this.nodes.length > 0) {
	            return 'fa fa-book';
	        }
	        return 'fa fa-file-text-o';
	    };
	    TreeNode.prototype.RenderHeader = function () {
	        var _this = this;
	        var treeheader = dom_1.Dom.div('treeheader');
	        treeheader.appendChild(dom_1.Dom.element('i', "treeitem-icon " + this.getIconClass(), { color: 'rgb(241,202,93)' }));
	        treeheader.appendChild(dom_1.Dom.text(this.info.title, 'span'));
	        if (this.nodes && this.nodes.length > 0) {
	            var span = dom_1.Dom.span("tree-toggle fa-stack fa-lg");
	            span.appendChild(dom_1.Dom.element('i', "tree-toggle-bgnd fa fa-circle fa-stack-2x"));
	            var icon = dom_1.Dom.element('i', 'tree-toggle-icon fa fa-stack-1x fa-chevron-' + (this.open ? 'up' : 'down'), { color: 'grey' });
	            span.appendChild(icon);
	            span.onclick = function (ev) {
	                _this.ToggleSubTree();
	            };
	            treeheader.appendChild(span);
	        }
	        if (this.level > 0) {
	            treeheader.style.cursor = 'pointer';
	            treeheader.onclick = function (ev) {
	                App_1.application.OnCommand({ command: 'OpenPage', param1: { guid: _this.info.guid } });
	            };
	        }
	        else {
	            if (this.info.commandInfo) {
	                treeheader.style.cursor = 'pointer';
	                treeheader.onclick = function (ev) {
	                    App_1.application.OnCommand(_this.info.commandInfo);
	                };
	            }
	        }
	        return treeheader;
	    };
	    TreeNode.prototype.RenderChilds = function () {
	        if (!this.nodes || this.nodes.length == 0) {
	            return undefined;
	        }
	        var treelist = dom_1.Dom.div('treelist');
	        this.nodes.forEach(function (node) { node.Render(treelist); });
	        treelist.style.display = this.open ? 'block' : 'none';
	        return treelist;
	    };
	    return TreeNode;
	}(UIElement_1.UIElement));
	exports.TreeNode = TreeNode;


/***/ },
/* 7 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var BoardsContext_1 = __webpack_require__(8);
	var BoxContext_1 = __webpack_require__(11);
	var ContentArea = (function () {
	    function ContentArea() {
	        this.boxContext = new BoxContext_1.BoxContext();
	        this.boardsContext = new BoardsContext_1.BoardsContext();
	        //treeheader.onclick = (ev:MouseEvent) => { application.OnCommand({command:'OpenPage', param1:{guid:this.info.guid}});
	    }
	    ContentArea.prototype.OnOpenBoards = function () {
	        this.boxContext.Hide();
	        this.boardsContext.LoadBoards();
	    };
	    ContentArea.prototype.OnOpenSavedBoards = function () {
	    };
	    ContentArea.prototype.OnOpenRecycledBoards = function () {
	    };
	    ContentArea.prototype.OnOpenPage = function (guid) {
	        //this.boardsContext.Hide();
	        //this.boxContext.LoadPage(guid);
	        //this.boxContext.Show();
	    };
	    ContentArea.prototype.OnBoxActivated = function (guid) {
	    };
	    ContentArea.prototype.OnBoxDeactivated = function (guid) {
	    };
	    ContentArea.prototype.OnBoxSizeChanged = function (guid, dimentions) {
	    };
	    ContentArea.prototype.OnBoxContentChanged = function (guid) {
	    };
	    ContentArea.prototype.SetRoot = function (_root) {
	        this.htmlElement = _root;
	        //self.onclick = (ev:MouseEvent) => { application.OnCommand({command:'HideSideBar'})};
	    };
	    ContentArea.prototype.Render = function () {
	        this.boxContext.Render(this.htmlElement);
	        this.boardsContext.Render(this.htmlElement);
	        this.boxContext.Hide();
	        //this.boardsContext.Show('flex');
	    };
	    return ContentArea;
	}());
	exports.ContentArea = ContentArea;


/***/ },
/* 8 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __extends = (this && this.__extends) || function (d, b) {
	    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
	    function __() { this.constructor = d; }
	    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
	};
	/// <reference path='../../typings/index.d.ts' />
	var dom_1 = __webpack_require__(4);
	var UIElement_1 = __webpack_require__(5);
	var BoardInfo_1 = __webpack_require__(9);
	var BoardsContext = (function (_super) {
	    __extends(BoardsContext, _super);
	    function BoardsContext() {
	        _super.call(this);
	        this.isLoading = false;
	        this.boardsInfo = {};
	    }
	    BoardsContext.prototype.LoadBoards = function () {
	        var _this = this;
	        this.isLoading = true;
	        $.ajax('/api/v1.0/boards', {
	            type: 'GET',
	            data: {},
	            dataType: 'json',
	            success: function (data, textStatus, jqXHR) { _this.OnBoardsLoaded(data, textStatus, jqXHR); }
	        });
	    };
	    BoardsContext.prototype.CreateDomImpl = function () {
	        var container = dom_1.Dom.div('board-container');
	        //create title
	        //create toolbar
	        return container;
	    };
	    BoardsContext.prototype.RenderSelf = function () {
	        if (this.isLoading) {
	            this.RenderLoadingState(this.htmlElement);
	        }
	        else {
	            this.RenderBoards(this.htmlElement);
	        }
	    };
	    BoardsContext.prototype.OnBoardsLoaded = function (data, textStatus, jqXHR) {
	        var _this = this;
	        this.isLoading = false;
	        this.boardsInfo = {};
	        if (data.boards && data.boards.length > 0) {
	            data.boards.forEach(function (board) { _this.boardsInfo[board.id] = new BoardInfo_1.BoardInfo(board); });
	        }
	        this.htmlElement.innerHTML = '';
	        this.RenderSelf();
	    };
	    BoardsContext.prototype.RenderBoards = function (self) {
	        var spinner = dom_1.Dom.GetElementByClassName(this.htmlElement, 'boards-spinner');
	        if (spinner) {
	            dom_1.Dom.Hide(spinner);
	        }
	        var add_board = dom_1.Dom.div('board-item add-board');
	        add_board.innerHTML = '  <span>Create New Board</span><div><img src="images/new_board.png"></div><i>+</i>';
	        self.appendChild(add_board);
	        for (var key in this.boardsInfo) {
	            this.boardsInfo[key].Render(self);
	        }
	    };
	    BoardsContext.prototype.RenderLoadingState = function (self) {
	        var spinner = dom_1.Dom.GetElementByClassName(self, 'boards-spinner');
	        if (spinner) {
	            dom_1.Dom.Show(spinner);
	            return;
	        }
	        var holder = dom_1.Dom.div('boards-spinner centered-box');
	        var outer_ball = dom_1.Dom.div('spinning_ball outer_ball');
	        var inner_ball = dom_1.Dom.div('spinning_ball inner_ball');
	        holder.appendChild(outer_ball);
	        holder.appendChild(inner_ball);
	        self.appendChild(holder);
	    };
	    return BoardsContext;
	}(UIElement_1.UIElement));
	exports.BoardsContext = BoardsContext;


/***/ },
/* 9 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __extends = (this && this.__extends) || function (d, b) {
	    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
	    function __() { this.constructor = d; }
	    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
	};
	var dom_1 = __webpack_require__(4);
	var DomElement_1 = __webpack_require__(10);
	var App_1 = __webpack_require__(1);
	var BoardInfo = (function (_super) {
	    __extends(BoardInfo, _super);
	    function BoardInfo(board) {
	        _super.call(this, { tag: 'div', className: 'board-item' });
	        this.board = board;
	    }
	    BoardInfo.prototype.Id = function () {
	        return this.board.id;
	    };
	    BoardInfo.prototype.Title = function () {
	        return this.board.title;
	    };
	    BoardInfo.prototype.RenderSelf = function (self) {
	        var _this = this;
	        var img = dom_1.Dom.img('images/boards/board' + this.Id() + '.png');
	        self.appendChild(img);
	        var title = dom_1.Dom.div();
	        title.innerText = this.Title();
	        self.appendChild(title);
	        self.onclick = function (ev) { App_1.application.OnCommand({ command: 'OpenBoard', param1: { guid: _this.Id() } }); };
	    };
	    return BoardInfo;
	}(DomElement_1.DomElement));
	exports.BoardInfo = BoardInfo;


/***/ },
/* 10 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var dom_1 = __webpack_require__(4);
	var DomElement = (function () {
	    function DomElement(props) {
	        this.props = props;
	    }
	    DomElement.prototype.RenderSelf = function (self) {
	    };
	    DomElement.prototype.Render = function (renderTo) {
	        if (!this.props.tag) {
	            return;
	        }
	        var element = dom_1.Dom.CreateFromProps(this.props);
	        this.RenderSelf(element);
	        if (renderTo) {
	            renderTo.appendChild(element);
	        }
	        return element;
	    };
	    return DomElement;
	}());
	exports.DomElement = DomElement;
	function RenderDomElement(props, renderTo) {
	    var element = new DomElement(props);
	    return element.Render(renderTo);
	}
	exports.RenderDomElement = RenderDomElement;


/***/ },
/* 11 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __extends = (this && this.__extends) || function (d, b) {
	    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
	    function __() { this.constructor = d; }
	    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
	};
	var dom_1 = __webpack_require__(4);
	var UIElement_1 = __webpack_require__(5);
	var Box_1 = __webpack_require__(12);
	var BoxContext = (function (_super) {
	    __extends(BoxContext, _super);
	    function BoxContext() {
	        _super.call(this);
	        this.isLoading = false;
	        this.boxes = {};
	        //TODO - delete
	        this.createFakeBoxes();
	    }
	    BoxContext.prototype.LoadPage = function (guid) {
	        var _this = this;
	        this.isLoading = true;
	        $.ajax('/api/v1.0/section:' + guid, {
	            type: 'GET',
	            data: {},
	            dataType: 'json',
	            success: function (data, textStatus, jqXHR) { _this.OnPageLoaded(data, textStatus, jqXHR); }
	        });
	    };
	    BoxContext.prototype.OnPageLoaded = function (data, textStatus, jqXHR) {
	        this.isLoading = false;
	        this.createFakeBoxes();
	    };
	    BoxContext.prototype.CreateDomImpl = function () {
	        var container = dom_1.Dom.div('content-container');
	        //create title
	        //create toolbar
	        return container;
	    };
	    BoxContext.prototype.RenderSelf = function () {
	        for (var guid in this.boxes) {
	            this.boxes[guid].Render(this.htmlElement);
	        }
	    };
	    BoxContext.prototype.OnBoxActivated = function (guid) {
	    };
	    BoxContext.prototype.OnBoxDeactivated = function (guid) {
	    };
	    BoxContext.prototype.OnBoxSizeChanged = function (guid, dimentions) {
	    };
	    BoxContext.prototype.OnBoxContentChanged = function (guid) {
	    };
	    BoxContext.prototype.createFakeBoxes = function () {
	        var _this = this;
	        var onBoxActivated = function (guid) { _this.OnBoxActivated(guid); };
	        var onBoxDeactivated = function (guid) { _this.OnBoxDeactivated(guid); };
	        var onBoxSizeChanged = function (guid, dimentions) { _this.OnBoxSizeChanged(guid, dimentions); };
	        var onBoxContentChanged = function (guid) { _this.OnBoxContentChanged(guid); };
	        for (var i = 0; i < 5; ++i) {
	            this.boxes['box1' + String(i)] = new Box_1.Box({
	                dimention: { x: 100 + 10 * i, y: 100 + 15 * i, w: 200, h: 40 },
	                info: { guid: 'box1' + String(i) },
	                callbacks: {
	                    boxActivated: onBoxActivated,
	                    boxDeactivated: onBoxDeactivated,
	                    sizeChanged: onBoxSizeChanged,
	                    contentChanged: onBoxContentChanged } });
	        }
	        this.boxes['box22'] = new Box_1.Box({
	            dimention: { x: 100, y: 300, w: 220, h: 40 },
	            info: { guid: 'box2' },
	            callbacks: {
	                boxActivated: onBoxActivated,
	                boxDeactivated: onBoxDeactivated,
	                sizeChanged: onBoxSizeChanged,
	                contentChanged: onBoxContentChanged } });
	    };
	    return BoxContext;
	}(UIElement_1.UIElement));
	exports.BoxContext = BoxContext;


/***/ },
/* 12 */
/***/ function(module, exports, __webpack_require__) {

	/// <reference path="../internal_typings/medium-editor.d.ts" />
	"use strict";
	var __extends = (this && this.__extends) || function (d, b) {
	    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
	    function __() { this.constructor = d; }
	    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
	};
	var dom_1 = __webpack_require__(4);
	var UIElement_1 = __webpack_require__(5);
	var BoxResizer_1 = __webpack_require__(13);
	var BoxDragger_1 = __webpack_require__(14);
	var BoxAction;
	(function (BoxAction) {
	    BoxAction[BoxAction["none"] = 0] = "none";
	    BoxAction[BoxAction["resize"] = 1] = "resize";
	    BoxAction[BoxAction["drag"] = 2] = "drag";
	})(BoxAction || (BoxAction = {}));
	var BoxState = (function () {
	    function BoxState() {
	    }
	    return BoxState;
	}());
	var Box = (function (_super) {
	    __extends(Box, _super);
	    function Box(props) {
	        var _this = this;
	        _super.call(this);
	        this.info = props.info;
	        this.state = new BoxState();
	        this.state.dimentions = props.dimention;
	        this.state.action = BoxAction.none;
	        this.state.activated = false;
	        this.callback = props.callbacks;
	        var onResizeStart = function (direction, clientX, clientY) {
	            _this.OnResizeStart(direction, clientX, clientY);
	        };
	        var onDragStart = function (clientX, clientY) {
	            _this.OnDragStart(clientX, clientY);
	        };
	        var onTouchMove = function (ev) { _this.OnTouchMove(ev); };
	        var onMouseMove = function (ev) { _this.OnMouseMove(ev.clientX, ev.clientY); };
	        var onMouseUp = function (ev) { _this.OnMouseUp(ev); };
	        this.horzResize = new BoxResizer_1.BoxResizer({ direction: BoxResizer_1.ResizeDirection.Horz, onResizeStart: onResizeStart });
	        this.dragger = new BoxDragger_1.BoxDragger({ onDragStart: onDragStart });
	        window.addEventListener('mouseup', onMouseUp);
	        window.addEventListener('mousemove', onMouseMove);
	        window.addEventListener('touchmove', onTouchMove);
	        window.addEventListener('touchend', onMouseUp);
	    }
	    Box.prototype.activate = function () {
	        if (this.state.activated == true) {
	            return;
	        }
	        this.setActivatedState(true);
	    };
	    Box.prototype.deactivate = function () {
	        this.setActivatedState(false);
	    };
	    Box.prototype.setActivatedState = function (activated) {
	        this.state.activated = activated;
	        this.changeActivationState();
	        if (this.state.activated == true && this.callback.boxActivated) {
	            this.callback.boxActivated(this.info.guid);
	        }
	        if (this.state.activated == false && this.callback.boxDeactivated) {
	            this.callback.boxDeactivated(this.info.guid);
	        }
	    };
	    Box.prototype.changeActivationState = function () {
	        var resizer = this.htmlElement.getElementsByClassName(BoxResizer_1.BoxResizer.getClass(this.horzResize.GetDirection()))[0];
	        if (resizer != undefined) {
	            resizer.style.display = this.state.activated ? 'block' : 'none';
	        }
	        var dragger = this.htmlElement.getElementsByClassName(BoxDragger_1.BoxDragger.getClass())[0];
	        if (dragger != undefined) {
	            dragger.style.display = this.state.activated ? 'block' : 'none';
	        }
	    };
	    Box.prototype.OnTouchMove = function (ev) {
	        this.OnMouseMove(ev.touches[0].clientX, ev.touches[0].clientY);
	    };
	    Box.prototype.OnResize = function (clientX, clientY) {
	        var _this = this;
	        var _a = this.state, dimentions = _a.dimentions, original = _a.original, action = _a.action, direction = _a.direction;
	        var newWidth = original.w;
	        if (direction == BoxResizer_1.ResizeDirection.Horz || direction == BoxResizer_1.ResizeDirection.Both) {
	            newWidth = original.w + clientX - original.x;
	        }
	        if (newWidth == dimentions.w) {
	            return;
	        }
	        this.state.dimentions.w = newWidth;
	        this.SetDimentions();
	        window.requestAnimationFrame(function (timestamp) { _this.OnUpdateHeight(); });
	    };
	    Box.prototype.OnDrag = function (clientX, clientY) {
	        var _a = this.state, dimentions = _a.dimentions, original = _a.original, action = _a.action, direction = _a.direction;
	        var newX = dimentions.x + clientX - original.x;
	        var newY = dimentions.y + clientY - original.y;
	        if (newX == dimentions.x && newY == dimentions.y) {
	            return;
	        }
	        this.state.dimentions.x = newX;
	        this.state.dimentions.y = newY;
	        this.state.original.x = clientX;
	        this.state.original.y = clientY;
	        this.SetDimentions();
	    };
	    Box.prototype.OnMouseMove = function (clientX, clientY) {
	        if (this.state.action == BoxAction.none) {
	            return;
	        }
	        if (this.state.action == BoxAction.resize) {
	            this.OnResize(clientX, clientY);
	        }
	        if (this.state.action == BoxAction.drag) {
	            this.OnDrag(clientX, clientY);
	        }
	    };
	    Box.prototype.OnMouseUp = function (ev) {
	        if (this.state.action == BoxAction.none) {
	            return;
	        }
	        if (this.state.action == BoxAction.resize && this.callback.sizeChanged) {
	            this.callback.sizeChanged(this.info.guid, this.state.dimentions);
	        }
	        if (this.state.action == BoxAction.drag && this.callback.sizeChanged) {
	            this.callback.sizeChanged(this.info.guid, this.state.dimentions);
	        }
	        this.state.action = BoxAction.none;
	    };
	    Box.prototype.OnMouseEnter = function () {
	        this.activate();
	    };
	    Box.prototype.OnMouseLeave = function () {
	        this.deactivate();
	    };
	    Box.prototype.SetDimentions = function () {
	        this.htmlElement.style.left = String(this.state.dimentions.x) + 'px';
	        this.htmlElement.style.top = String(this.state.dimentions.y) + 'px';
	        this.htmlElement.style.width = String(this.state.dimentions.w) + 'px';
	        this.htmlElement.style.height = String(this.state.dimentions.h) + 'px';
	    };
	    Box.prototype.CreateDomImpl = function () {
	        var _this = this;
	        var box = dom_1.Dom.div('internal-box');
	        box.onmouseenter = function (ev) { _this.OnMouseEnter(); };
	        box.onmouseleave = function (ev) { _this.OnMouseLeave(); };
	        return box;
	    };
	    Box.prototype.OnContentChanged = function (content) {
	        var style = window.getComputedStyle(content, null);
	        if (parseInt(style.height) + 20 != this.state.dimentions.h) {
	            this.state.dimentions.h = parseInt(style.height) + 20;
	            this.htmlElement.style.height = String(this.state.dimentions.h) + 'px';
	        }
	    };
	    Box.prototype.OnUpdateHeight = function () {
	        var content = this.htmlElement.getElementsByClassName('internal-box-content')[0];
	        var style = window.getComputedStyle(content, null);
	        if (parseInt(style.height) + 20 != this.state.dimentions.h) {
	            this.state.dimentions.h = parseInt(style.height) + 20;
	            this.state.original.h = this.state.dimentions.h;
	            this.htmlElement.style.height = String(this.state.dimentions.h) + 'px';
	        }
	    };
	    Box.prototype.RenderSelf = function () {
	        var _this = this;
	        this.SetDimentions();
	        this.horzResize.Render(this.htmlElement);
	        this.dragger.Render(this.htmlElement);
	        var content = dom_1.Dom.div('internal-box-content');
	        this.htmlElement.appendChild(content);
	        var editor = new MediumEditor([content], {
	            buttonLabels: 'fontawesome',
	            toolbar: {
	                buttons: [
	                    'bold',
	                    'italic',
	                    'table'
	                ]
	            }
	        });
	        editor.subscribe('editableInput', function (data, editable) { _this.OnContentChanged(editable); });
	    };
	    Box.prototype.OnDragStart = function (clientX, clientY) {
	        this.state.action = BoxAction.drag;
	        this.state.original = { x: clientX, y: clientY, w: this.state.dimentions.w, h: this.state.dimentions.h };
	    };
	    Box.prototype.OnResizeStart = function (direction, clientX, clientY) {
	        this.state.action = BoxAction.resize;
	        this.state.direction = direction;
	        this.state.original = { x: clientX, y: clientY, w: this.state.dimentions.w, h: this.state.dimentions.h };
	    };
	    return Box;
	}(UIElement_1.UIElement));
	exports.Box = Box;


/***/ },
/* 13 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __extends = (this && this.__extends) || function (d, b) {
	    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
	    function __() { this.constructor = d; }
	    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
	};
	var DomElement_1 = __webpack_require__(10);
	(function (ResizeDirection) {
	    ResizeDirection[ResizeDirection["Horz"] = 0] = "Horz";
	    ResizeDirection[ResizeDirection["Vert"] = 1] = "Vert";
	    ResizeDirection[ResizeDirection["Both"] = 2] = "Both";
	})(exports.ResizeDirection || (exports.ResizeDirection = {}));
	var ResizeDirection = exports.ResizeDirection;
	var BoxResizer = (function (_super) {
	    __extends(BoxResizer, _super);
	    function BoxResizer(resizerProps) {
	        var _this = this;
	        _super.call(this, {
	            tag: 'div',
	            className: BoxResizer.getClass(resizerProps.direction)
	        });
	        this.onTouchStart = function (event) {
	            var te = event.touches[0];
	            if (_this.resizerProps.onResizeStart) {
	                _this.resizerProps.onResizeStart(_this.resizerProps.direction, te.clientX, te.clientY);
	            }
	        };
	        this.onResizeStart = function (event) {
	            if (_this.resizerProps.onResizeStart) {
	                _this.resizerProps.onResizeStart(_this.resizerProps.direction, event.clientX, event.clientY);
	            }
	        };
	        this.resizerProps = resizerProps;
	    }
	    BoxResizer.prototype.GetDirection = function () {
	        return this.resizerProps.direction;
	    };
	    BoxResizer.getClass = function (type) {
	        switch (type) {
	            case ResizeDirection.Horz:
	                return 'resizer_horz';
	            case ResizeDirection.Vert:
	                return 'resizer_vert';
	            case ResizeDirection.Both:
	                return 'resizer_both';
	        }
	    };
	    BoxResizer.prototype.RenderSelf = function (self) {
	        self.onmousedown = this.onResizeStart;
	        self.ontouchstart = this.onTouchStart;
	    };
	    return BoxResizer;
	}(DomElement_1.DomElement));
	exports.BoxResizer = BoxResizer;


/***/ },
/* 14 */
/***/ function(module, exports, __webpack_require__) {

	"use strict";
	var __extends = (this && this.__extends) || function (d, b) {
	    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
	    function __() { this.constructor = d; }
	    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
	};
	var DomElement_1 = __webpack_require__(10);
	var BoxDragger = (function (_super) {
	    __extends(BoxDragger, _super);
	    function BoxDragger(draggerProps) {
	        var _this = this;
	        _super.call(this, {
	            tag: 'div',
	            className: BoxDragger.getClass()
	        });
	        this.onTouchStart = function (event) {
	            var te = event.touches[0];
	            if (_this.draggerProps.onDragStart) {
	                _this.draggerProps.onDragStart(te.clientX, te.clientY);
	            }
	        };
	        this.onDragStart = function (event) {
	            console.log('onDragStart');
	            if (_this.draggerProps.onDragStart) {
	                console.log('firing dragStart');
	                _this.draggerProps.onDragStart(event.clientX, event.clientY);
	            }
	        };
	        this.draggerProps = draggerProps;
	    }
	    BoxDragger.getClass = function () {
	        return 'box_dragger';
	    };
	    BoxDragger.prototype.RenderSelf = function (self) {
	        self.onmousedown = this.onDragStart;
	        self.ontouchstart = this.onTouchStart;
	    };
	    return BoxDragger;
	}(DomElement_1.DomElement));
	exports.BoxDragger = BoxDragger;


/***/ }
/******/ ]);
//# sourceMappingURL=boards.js.map