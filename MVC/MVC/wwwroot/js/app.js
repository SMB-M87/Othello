"use strict";function _typeof(e){"@babel/helpers - typeof";return(_typeof="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(e){return typeof e}:function(e){return e&&"function"==typeof Symbol&&e.constructor===Symbol&&e!==Symbol.prototype?"symbol":typeof e})(e)}function _classCallCheck(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function _defineProperties(e,t){for(var n=0;n<t.length;n++){var a=t[n];a.enumerable=a.enumerable||!1,a.configurable=!0,"value"in a&&(a.writable=!0),Object.defineProperty(e,_toPropertyKey(a.key),a)}}function _createClass(e,t,n){return t&&_defineProperties(e.prototype,t),n&&_defineProperties(e,n),Object.defineProperty(e,"prototype",{writable:!1}),e}function _toPropertyKey(e){var t=_toPrimitive(e,"string");return"symbol"==_typeof(t)?t:t+""}function _toPrimitive(e,t){if("object"!=_typeof(e)||!e)return e;var n=e[Symbol.toPrimitive];if(void 0!==n){var a=n.call(e,t||"default");if("object"!=_typeof(a))return a;throw new TypeError("@@toPrimitive must return a primitive value.")}return("string"===t?String:Number)(e)}var FeedbackSingleton=function(){var e;return{getInstance:function(){return e||(e=new FeedbackWidget("feedback-widget")),e}}}(),FeedbackWidget=function(){function e(t){_classCallCheck(this,e),this._elementId=t,this._timeout=null}return _createClass(e,[{key:"elementId",get:function(){return this._elementId}},{key:"show",value:function(e,t){var n=this,a=arguments.length>2&&void 0!==arguments[2]?arguments[2]:6e3,o=!(arguments.length>3&&void 0!==arguments[3])||arguments[3],i=arguments.length>4&&void 0!==arguments[4]?arguments[4]:[],r=$("#"+this._elementId),c=$("#feedback-section"),l=$('<button class="feedback-widget__close">×</button>');c.addClass("flex").removeClass("hidden"),r.empty().append(l).append("<span>".concat(e,"</span>"));var s=$('<div class="feedback-widget__actions"></div>');i.forEach(function(e){var t=$("<span>".concat(e.icon,"</span>"));t.addClass(e.class),t.on("click",e.callback),s.append(t)}),r.append(s),r.removeClass("hidden fade-out").addClass("visible alert alert-".concat(t.toLowerCase()," fade-in")),o&&(this._timeout&&clearTimeout(this._timeout),this._timeout=setTimeout(function(){return n.hide()},a)),l.on("click",function(){return n.hide()}),this.log({message:e,type:t})}},{key:"hide",value:function(){var e=$("#"+this._elementId),t=$("#feedback-section");e.removeClass("fade-in").addClass("fade-out"),setTimeout(function(){e.addClass("hidden").removeClass("visible fade-out").empty(),t.addClass("hidden").removeClass("flex")},500),this._timeout&&clearTimeout(this._timeout)}},{key:"log",value:function(e){var t=JSON.parse(localStorage.getItem("feedback_widget"))||{messages:[]};t.messages.unshift(e),t.messages.length>10&&t.messages.pop(),localStorage.setItem("feedback_widget",JSON.stringify(t))}},{key:"removeLog",value:function(){localStorage.removeItem("feedback_widget")}},{key:"history",value:function(){var e=JSON.parse(localStorage.getItem("feedback_widget")),t=(null===e||void 0===e?void 0:e.messages.map(function(e){return"".concat(e.type," - ").concat(e.message)}).join("\n"))||"No history.";console.log(t)}}])}(),Game=function(e){if(!(e&&e.apiUrl&&e.userToken&&e.redirectUrl))throw new Error("Game module initialization failed: Missing config properties.");var t={apiUrl:e.apiUrl,apiKey:e.userToken,redirectUrl:e.redirectUrl},n=function(){Game.Handlebar.renderBody(),Game.Handlebar.renderBoard(null,null,null),Game.Handlebar.attachEventListeners(),Game.Data.init(t.apiUrl,t.apiKey,"production"),Game.Api.init(),Game.Stat.init("stats-chart")},a=function(){setInterval(function(){Game.Model.getGameState()},1e3)};return{init:function(e){n(),a(),e&&e()}}}(config);$(function(){function e(){}Game.init(e)}),Game.Handlebar=function(){var e=spa_templates||{},t=function(t,n,a){if(!e[t])throw new Error("Template '".concat(t,"' is not available."));var o=e[t](n||{}),i=document.getElementById(a);if(!i)throw new Error("Container with ID '".concat(a,"' not found."));i.innerHTML=o},n=function(e,t,n){for(var a=[],o=n||1,i=0;i<8;i++){for(var r=[],c=0;c<8;c++){var l,s,d=e&&null!==(l=null===(s=e[i])||void 0===s?void 0:s[c])&&void 0!==l?l:0,u=e&&t===n&&3===d,m=e&&(1===d||2===d),f=1===d?"white-piece":2===d?"black-piece":"";r.push({row:i,col:c,cellClass:(i+c)%2==0?"even":"odd",randomX:Math.random(),randomY:Math.random(),randomRot:Math.random(),animationDelay:.02*(8*i+c),isPossibleMove:u||!1,hasPiece:m||!1,pieceColorClass:f,playerColorClass:1===o?"white":"black",highlight:!1,flip:!1})}a.push(r)}return a},a=function(e){e.forEach(function(e){var t=e.selector,n=e.event,a=e.callback,o=document.querySelector(t);o&&o.addEventListener(n,a)})};return{renderBody:function(e){t("body",e,"body")},renderBoard:function(e,a,o){var i=n(e,a,o);t("board",{boardRows:i},"game-board-container")},attachEventListeners:function(){a([{selector:"#game-board-container",event:"click",callback:function(e){var t=e.target.closest("td");if(t){var n=t.dataset.row,a=t.dataset.col;Game.Data.sendMove(n,a).then(function(){FeedbackSingleton.getInstance().log({message:"Move made on row ".concat(n," and col ").concat(a,"."),type:"Success"})}).catch(function(e){FeedbackSingleton.getInstance().log({message:"Move failed: ".concat(e.responseText||e),type:"Danger"})})}}},{selector:"#pass-button",event:"click",callback:function(){Game.Data.passGame().then(function(){FeedbackSingleton.getInstance().log({message:"Turn passed.",type:"Success"})}).catch(function(e){FeedbackSingleton.getInstance().log({message:"Pass failed: ".concat(e.responseText||e),type:"Danger"})})}},{selector:"#forfeit-button",event:"click",callback:function(){FeedbackSingleton.getInstance().show("Are you sure you want to forfeit?","info",8e3,!0,[{icon:"✓",class:"feedback-icon feedback-icon--success",callback:function(){Game.Data.forfeitGame().then(function(){FeedbackSingleton.getInstance().removeLog(),FeedbackSingleton.getInstance().hide()}).catch(function(e){FeedbackSingleton.getInstance().log({message:"Forfeit failed: ".concat(e.responseText||e),type:"Danger"})})}},{icon:"✕",class:"feedback-icon feedback-icon--danger",callback:function(){FeedbackSingleton.getInstance().hide()}}])}},{selector:"#rematch-button",event:"click",callback:function(){Game.Data.rematchGame().then(function(){FeedbackSingleton.getInstance().removeLog(),Game.Data.redirectHome()}).catch(function(e){FeedbackSingleton.getInstance().log({message:"Rematch failed: ".concat(e.responseText||e),type:"Danger"})})}}])}}}(),Game.Data=function(){var e={apiUrl:null,apiKey:null,mock:[{url:null,key:null}]},t={environment:""},n=function(n,a,o){if("production"==o)e.apiUrl=n,e.apiKey=a,t.environment=o;else{if("development"!=o)throw new Error("This environment is unknown.");e.mock.url=n,e.mock.key=a,t.environment=o}},a=function(){if("production"==t.environment)return $.get(e.apiUrl+"game/view/"+e.apiKey).then(function(e){return e}).catch(function(e){console.log(e.message)});if("development"==t.environment)return p();throw new Error("This environment is unknown.")},o=function(){if("production"==t.environment)return $.get(e.apiUrl+"game/partial/"+e.apiKey).then(function(e){return e}).catch(function(e){console.log(e.message)});if("development"==t.environment)return p();throw new Error("This environment is unknown.")},i=function(n){if("production"==t.environment)return new Promise(function(t,a){setTimeout(function(){$.get(e.apiUrl+"result/last/"+e.apiKey).then(function(e){return t(e)}).catch(function(e){console.log(e.message),a(e)})},n)});if("development"==t.environment)return p();throw new Error("This environment is unknown.")},r=function(){var t=Game.Model.getOpponent(),n="".concat(t," ").concat(e.apiKey),a=encodeURIComponent(n);return $.ajax({url:"".concat(e.apiUrl,"player/rematch/").concat(a),method:"GET"}).then(function(e){return e||null}).catch(function(e){return null})},c=function(t,n){var a=e.apiKey,o={playerToken:a,row:t,column:n};return $.ajax({url:e.apiUrl+"game/move",method:"POST",contentType:"application/json",data:JSON.stringify(o)}).then(function(e){Game.Model.getGameState()})},l=function(){var t=e.apiKey;return $.ajax({url:e.apiUrl+"game/pass",method:"POST",contentType:"application/json",data:JSON.stringify({token:t})}).then(function(e){Game.Model.getGameState()})},s=function(){var t=e.apiKey;return $.ajax({url:e.apiUrl+"game/forfeit",method:"POST",contentType:"application/json",data:JSON.stringify({token:t})}).then(function(e){Game.Model.getGameState()})},d=function(){var t=Game.Model.getOpponent(),n={PlayerToken:e.apiKey,Description:"Rematch against ".concat(t),Rematch:t};return $.ajax({url:e.apiUrl+"game/create",method:"POST",contentType:"application/json",data:JSON.stringify(n)})},u=function(){var t=Game.Model.getOpponent(),n={ReceiverUsername:t,SenderToken:e.apiKey};return $.ajax({url:e.apiUrl+"player/request/game/accept",method:"POST",contentType:"application/json",data:JSON.stringify(n)}).then(function(e){})},m=function(){var t=Game.Model.getOpponent(),n={ReceiverUsername:t,SenderToken:e.apiKey};return $.ajax({url:e.apiUrl+"player/request/game/decline",method:"POST",contentType:"application/json",data:JSON.stringify(n)}).then(function(e){})},f=function(t){e.mock=t},p=function(){var t=e.mock;return new Promise(function(e,n){e(t)})},g=function(){window.location.href="".concat(e.redirectUrl,"Home/Index")},h=function(e,t,a){n(e,t,a)},v=function(){return a()},y=function(){return o()},b=function(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:1e3;return i(e)},k=function(){return r()},w=function(e,t){return c(e,t)},L=function(){return l()},S=function(){return s()},G=function(){return d()},C=function(){return u()},I=function(){return m()},T=function(e){f(e)};return{init:h,get:v,getPartial:y,getResult:b,getRematch:k,sendMove:w,passGame:L,forfeitGame:S,rematchGame:G,acceptGame:C,declineGame:I,redirectHome:function(){g()},setMockData:T}}(),Game.Model=function(){var e={firstLoad:!0,turnReload:!0,lastLoad:!0,endLoad:!0,rematchLoad:!0,opponent:"",playerColor:"",board:null},t=function(){return(e.endLoad?e.lastLoad?e.firstLoad?Game.Data.get():Game.Data.getPartial():Game.Data.getResult():Game.Data.getRematch()).then(function(t){if(e.endLoad)!e.lastLoad&&e.endLoad?(n(t),e.endLoad=!1):e.firstLoad?(a(t.opponent,t.color,t.partial.time,t.partial.playersturn,t.partial.board),e.opponent=t.opponent,e.playerColor=t.color,e.firstLoad=!1):0!==t.playersTurn&&t.playersTurn===e.playerColor?(r(e.playerColor,t.time,t.playersTurn),e.turnReload&&(o(e.playerColor,t.playersTurn,t.board),e.turnReload=!1)):0!==t.playersTurn&&t.playersTurn!==e.playerColor?(r(e.playerColor,t.time,t.playersTurn),e.turnReload||(o(e.playerColor,t.playersTurn,t.board),e.turnReload=!0)):(!t.inGame||0===t.playersTurn&&t.inGame)&&e.lastLoad&&(r(e.playerColor,0,t.playersTurn),o(e.playerColor,0,t.board),e.lastLoad=!1);else if(t===e.opponent&&e.rematchLoad){var i=FeedbackSingleton.getInstance();i.show("".concat(e.opponent," wants a rematch, do you accept?"),"info",45e3,!0,[{icon:"✓",class:"feedback-icon feedback-icon--success",callback:function(){Game.Data.acceptGame(),i.hide(),window.location.reload()}},{icon:"✕",class:"feedback-icon feedback-icon--danger",callback:function(){Game.Data.declineGame(),i.hide()}}]),e.rematchLoad=!1}return t}).catch(function(e){return console.log(e.message),{data:null,error:e.message}})},n=function(t){document.getElementById("game-status").textContent=t.draw?"Drew":t.winner===e.opponent?"Lost":"Won",t.forfeit&&(document.getElementById("forfeit-title").textContent="by forfeit"),r(0,0,0),Game.Othello.updateBoard(t.board,0,e.playerColor)},a=function(e,t,n,a,o){i(e,t),r(t,n,a),Game.Othello.updateBoard(o,a,t)},o=function(e,t,n){Game.Othello.updateBoard(n,t,e),c(e,!!n&&n.some(function(e){return e.includes(3)}),t),t!==e&&Game.Api.get()},i=function(e,t){var n=document.getElementById("player-info");document.getElementById("opponent-name").textContent=e,n.classList.add("flex","fade-in"),document.getElementById("stat-button").classList.add("fade-in");var a=document.getElementById("player-color-indicator"),o=document.getElementById("opponent-color-indicator");a.classList.remove("white","black"),o.classList.remove("white","black"),1===t?(a.classList.add("white"),o.classList.add("black")):(a.classList.add("black"),o.classList.add("white")),document.getElementById("score-display").classList.add("flex","fade-in"),document.getElementById("player-score").id=1===t?"white-score":"black-score",document.getElementById("opponent-score").id=1===t?"black-score":"white-score"},r=function(e,t,n){var a=document.getElementById("timer-color-indicator");0!==n?(e===n?(a.classList.remove("red"),a.classList.add("green"),t.endsWith("0")||t.endsWith("5")||"4"===t||"3"===t||"2"===t||"1"===t?a.classList.add("wobble"):a.classList.remove("wobble")):(a.classList.remove("green"),a.classList.add("red")),document.getElementById("time-remaining").textContent=t):(a.classList.remove("green","red"),a.classList.add("fade-out"))},c=function(e,t,n){var a=document.getElementById("pass-button"),o=document.getElementById("forfeit-button");if(0===n){var i=document.getElementById("rematch-button");a.classList.add("fade-out"),a.classList.remove("fade-in-wobble"),o.classList.add("fade-out"),o.classList.remove("inline-block","fade-in"),i.classList.add("inline-block","fade-in"),i.classList.remove("fade-out")}else e===n?(t?(a.classList.add("fade-out"),a.classList.remove("fade-in-wobble")):(a.classList.add("fade-in-wobble"),a.classList.remove("fade-out")),o.classList.add("fade-in"),o.classList.remove("fade-out")):(a.classList.add("fade-out"),a.classList.remove("fade-in-wobble"),o.classList.add("fade-out"),o.classList.remove("fade-in"))},l=function(t){e.board=t},s=function(){return e.board},d=function(){return e.opponent};return{getGameState:function(){return t()},setBoard:function(e){return l(e)},getBoard:function(){return s()},getOpponent:function(){return d()}}}(),Game.Othello=function(){var e=function(e,t,n){var a=0,o=0,i=Game.Model.getBoard()||[];document.querySelectorAll("#game-board-container td").forEach(function(r){var c,l,s,d=parseInt(r.dataset.row,10),u=parseInt(r.dataset.col,10),m=null!==(c=null===(l=e[d])||void 0===l?void 0:l[u])&&void 0!==c?c:0,f=(null===(s=i[d])||void 0===s?void 0:s[u])||0,p=r.querySelector(".cell-div");if(1===m?a++:2===m&&o++,t!==n&&3===f&&(p.innerHTML=""),0===t)if(p.innerHTML="",!e||1!==m&&2!==m){if(1===f||2===f){var g=document.createElement("i");g.className="fa fa-circle ".concat(1===f?"white-piece":"black-piece"),p.appendChild(g)}}else{var h=document.createElement("i");h.className="fa fa-circle ".concat(1===m?"white-piece":"black-piece"),p.appendChild(h)}else if(t===n&&3===m){p.innerHTML="";var v=document.createElement("div");v.className="possible-move ".concat(1===n?"white-border":"black-border"),p.appendChild(v),r.appendChild(p)}else if(m===f||1!==m&&2!==m){if(m===f&&(1===m||2===m)){var y=r.querySelector("i");y.classList.remove("highlight","flip")}}else{var b=r.querySelector("i");if(b)b.classList.remove("highlight"),b.classList.add("flip"),setTimeout(function(){b.classList.remove("white-piece","black-piece"),b.classList.add(1===m?"white-piece":"black-piece")},300);else{p.innerHTML="";var k=document.createElement("i");k.className="fa fa-circle ".concat(1===m?"white-piece":"black-piece"," highlight"),p.appendChild(k)}}}),document.getElementById("white-score").textContent=a,document.getElementById("black-score").textContent=o,Game.Stat.updateStats(a,o),Game.Model.setBoard(e)};return{updateBoard:function(t,n,a){e(t,n,a)}}}(),Game.Api=function(){var e={apiUrl:null},t=function(){e.apiUrl="https://geek-jokes.sameerkumar.website/api?format=json",n()},n=function(){$.get(e.apiUrl).then(function(e){a(e)}).catch(function(e){console.log(e.message)})},a=function(e){var t=document.getElementById("joke-content");t&&(e&&e.joke?t.textContent=e.joke:t.textContent="string"==typeof e?e:"No joke available at the moment!")};return{init:function(){t()},get:function(){n()}}}(),Game.Stat=function(){var e={turn:0,chartContainer:null,chartInstance:null,statsData:{labels:[],capturedPieces:{white:[],black:[]}}},t=function(t){if(!t)throw new Error("Game.Stat init failed: No container ID provided.");e.chartContainer=t,n()},n=function(){var t=document.getElementById(e.chartContainer).getContext("2d");e.chartInstance&&e.chartInstance.destroy(),e.chartInstance=new Chart(t,{type:"line",data:{labels:e.statsData.labels,datasets:[{label:"White",data:e.statsData.capturedPieces.white,borderColor:"#d0c292",fill:!1},{label:"Black",data:e.statsData.capturedPieces.black,borderColor:"#000000",fill:!1}]},options:{responsive:!0,plugins:{legend:{display:!0}},scales:{x:{title:{display:!0,text:"Turns"}},y:{title:{display:!0,text:"Count"}}}}})},a=function(t,a){e.statsData.labels.push("".concat(e.turn)),e.statsData.capturedPieces.white.push(t),e.statsData.capturedPieces.black.push(a),e.turn++,n()};return{init:function(e){t(e)},updateStats:function(e,t,n){a(e,t,n)}}}();