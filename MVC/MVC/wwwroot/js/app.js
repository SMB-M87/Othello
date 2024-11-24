"use strict";function _typeof(e){"@babel/helpers - typeof";return(_typeof="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(e){return typeof e}:function(e){return e&&"function"==typeof Symbol&&e.constructor===Symbol&&e!==Symbol.prototype?"symbol":typeof e})(e)}function _classCallCheck(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function _defineProperties(e,t){for(var n=0;n<t.length;n++){var a=t[n];a.enumerable=a.enumerable||!1,a.configurable=!0,"value"in a&&(a.writable=!0),Object.defineProperty(e,_toPropertyKey(a.key),a)}}function _createClass(e,t,n){return t&&_defineProperties(e.prototype,t),n&&_defineProperties(e,n),Object.defineProperty(e,"prototype",{writable:!1}),e}function _toPropertyKey(e){var t=_toPrimitive(e,"string");return"symbol"==_typeof(t)?t:t+""}function _toPrimitive(e,t){if("object"!=_typeof(e)||!e)return e;var n=e[Symbol.toPrimitive];if(void 0!==n){var a=n.call(e,t||"default");if("object"!=_typeof(a))return a;throw new TypeError("@@toPrimitive must return a primitive value.")}return("string"===t?String:Number)(e)}var FeedbackSingleton=function(){var e;return{getInstance:function(){return e||(e=new FeedbackWidget("feedback-widget")),e}}}(),FeedbackWidget=function(){function e(t){_classCallCheck(this,e),this._elementId=t,this._timeout=null}return _createClass(e,[{key:"elementId",get:function(){return this._elementId}},{key:"show",value:function(e,t){var n=this,a=arguments.length>2&&void 0!==arguments[2]?arguments[2]:6e3,o=!(arguments.length>3&&void 0!==arguments[3])||arguments[3],r=arguments.length>4&&void 0!==arguments[4]?arguments[4]:[],i=$("#"+this._elementId),s=$("#feedback-section"),l=$('<button class="feedback-widget__close">×</button>');s.addClass("flex").removeClass("hidden"),i.empty().append(l).append("<span>".concat(e,"</span>"));var c=$('<div class="feedback-widget__actions"></div>');r.forEach(function(e){var t=$("<span>".concat(e.icon,"</span>"));t.addClass(e.class),t.on("click",e.callback),c.append(t)}),i.append(c),i.removeClass("hidden fade-out").addClass("visible alert alert-".concat(t.toLowerCase()," fade-in")),o&&(this._timeout&&clearTimeout(this._timeout),this._timeout=setTimeout(function(){return n.hide()},a)),l.on("click",function(){return n.hide()}),this.log({message:e,type:t})}},{key:"hide",value:function(){var e=$("#"+this._elementId),t=$("#feedback-section");e.removeClass("fade-in").addClass("fade-out"),setTimeout(function(){e.addClass("hidden").removeClass("visible fade-out").empty(),t.addClass("hidden").removeClass("flex")},500),this._timeout&&clearTimeout(this._timeout)}},{key:"log",value:function(e){var t=JSON.parse(localStorage.getItem("feedback_widget"))||{messages:[]};t.messages.unshift(e),t.messages.length>10&&t.messages.pop(),localStorage.setItem("feedback_widget",JSON.stringify(t))}},{key:"removeLog",value:function(){localStorage.removeItem("feedback_widget")}},{key:"history",value:function(){var e=JSON.parse(localStorage.getItem("feedback_widget")),t=(null===e||void 0===e?void 0:e.messages.map(function(e){return"".concat(e.type," - ").concat(e.message)}).join("\n"))||"No history.";console.log(t)}}])}(),Game=function(e){if(!(e&&e.apiUrl&&e.userToken&&e.redirectUrl))throw new Error("Game module initialization failed: Missing config properties.");var t={apiUrl:e.apiUrl,apiKey:e.userToken,redirectUrl:e.redirectUrl},n=function(){a(),o(),Game.Data.init(t.apiUrl,t.apiKey,"production")},a=function(){if(!spa_templates||!spa_templates.body)throw new Error("Template 'body' is not available. Ensure the template is compiled and loaded.");var e=spa_templates.body();document.getElementById("body").innerHTML=e},o=function(){var e=FeedbackSingleton.getInstance();e.removeLog(),$("#game-board-container").on("click",".possible-move",function(t){var n=$(t.target).closest("td"),a=n.data("row"),o=n.data("col");Game.Data.sendMove(a,o).then(function(){e.log({message:"Move made on row ".concat(a," and col ").concat(o,"."),type:"Success"}),e.history()}).catch(function(t){e.log({message:"Move failed: ".concat(t.responseText||t),type:"Danger"})})}),$("#pass-button").on("click",function(){Game.Data.passGame().then(function(){e.log({message:"Turn passed.",type:"Success"}),e.history()}).catch(function(t){e.log({message:"Pass failed: "+t.responseText,type:"Danger"})})}),$("#forfeit-button").on("click",function(){e.show("Are you sure you want to forfeit?","info",8e3,!0,[{icon:"✓",class:"feedback-icon feedback-icon--success",callback:function(){Game.Data.forfeitGame().then(function(){e.removeLog(),e.hide()}).catch(function(t){e.log("Forfeit failed: "+t.responseText,"Danger")})}},{icon:"✕",class:"feedback-icon feedback-icon--danger",callback:function(){e.hide()}}]),$("#rematch-button").on("click",function(){Game.Data.rematchGame().then(function(){window.location.href="".concat(t.redirectUrl,"Home/Index")}).catch(function(t){e.log({message:"Rematch failed: "+t.responseText,type:"Danger"})})})})},r=function(){Game.Othello.board(),setInterval(function(){Game.Model.getGameState()},1e3)};return{init:function(e){n(),r(),e&&e()}}}(config);$(function(){function e(){}Game.init(e)}),Game.Data=function(){var e={apiUrl:null,apiKey:null,mock:[{url:null,key:null}]},t={environment:""},n=function(n,a,o){if("production"==o)e.apiUrl=n,e.apiKey=a,t.environment=o;else{if("development"!=o)throw new Error("This environment is unknown.");e.mock.url=n,e.mock.key=a,t.environment=o}},a=function(){if("production"==t.environment)return $.get(e.apiUrl+"game/view/"+e.apiKey).then(function(e){return e}).catch(function(e){console.log(e.message)});if("development"==t.environment)return p();throw new Error("This environment is unknown.")},o=function(){if("production"==t.environment)return $.get(e.apiUrl+"game/partial/"+e.apiKey).then(function(e){return e}).catch(function(e){console.log(e.message)});if("development"==t.environment)return p();throw new Error("This environment is unknown.")},r=function(){var t=Game.Model.getOpponent(),n="".concat(t," ").concat(e.apiKey),a=encodeURIComponent(n);return $.ajax({url:"".concat(e.apiUrl,"player/rematch/").concat(a),method:"GET"}).then(function(e){return e||null}).catch(function(e){return null})},i=function(t,n){var a=e.apiKey,o={playerToken:a,row:t,column:n};return $.ajax({url:e.apiUrl+"game/move",method:"POST",contentType:"application/json",data:JSON.stringify(o)}).then(function(e){Game.Model.getGameState()})},s=function(){var t=e.apiKey;return $.ajax({url:e.apiUrl+"game/pass",method:"POST",contentType:"application/json",data:JSON.stringify({token:t})}).then(function(e){Game.Model.getGameState()})},l=function(){var t=e.apiKey;return $.ajax({url:e.apiUrl+"game/forfeit",method:"POST",contentType:"application/json",data:JSON.stringify({token:t})}).then(function(e){Game.Model.getGameState()})},c=function(){var t=Game.Model.getOpponent(),n={PlayerToken:e.apiKey,Description:"Rematch against ".concat(t),Rematch:t};return $.ajax({url:e.apiUrl+"game/create",method:"POST",contentType:"application/json",data:JSON.stringify(n)})},d=function(){var t=Game.Model.getOpponent(),n={ReceiverUsername:t,SenderToken:e.apiKey};return $.ajax({url:e.apiUrl+"player/request/game/accept",method:"POST",contentType:"application/json",data:JSON.stringify(n)}).then(function(e){})},u=function(){var t=Game.Model.getOpponent(),n={ReceiverUsername:t,SenderToken:e.apiKey};return $.ajax({url:e.apiUrl+"player/request/game/decline",method:"POST",contentType:"application/json",data:JSON.stringify(n)}).then(function(e){})},m=function(t){e.mock=t},p=function(){var t=e.mock;return new Promise(function(e,n){e(t)})};return{init:function(e,t,a){n(e,t,a)},get:function(){return a()},getPartial:function(){return o()},getRematch:function(){return r()},sendMove:function(e,t){return i(e,t)},passGame:function(){return s()},forfeitGame:function(){return l()},rematchGame:function(){return c()},acceptGame:function(){return d()},declineGame:function(){return u()},setMockData:function(e){m(e)}}}(),Game.Model=function(){var e={firstLoad:!0,turnReload:!0,lastLoad:!0,endLoad:!1,rematchLoad:!0,opponent:"",playerColor:"",board:null,winner:0},t=function(){return(e.endLoad?Game.Data.getRematch():e.firstLoad?Game.Data.get():Game.Data.getPartial()).then(function(t){if(e.endLoad){if(null!=t&&e.rematchLoad){var r=FeedbackSingleton.getInstance(),i=document.getElementById("feedback-section"),s=document.getElementById("feedback-widget");i.classList.add("transparent"),s.classList.add("top-right"),r.show("".concat(e.opponent," wants a rematch, do you accept?"),"info",45e3,!0,[{icon:"✓",class:"feedback-icon feedback-icon--success",callback:function(){Game.Data.acceptGame(),r.hide(),window.location.reload()}},{icon:"✕",class:"feedback-icon feedback-icon--danger",callback:function(){Game.Data.declineGame(),r.hide()}}]),e.rematchLoad=!1}}else e.firstLoad?(e.opponent=t.opponent,e.playerColor=t.color,e.board=t.partial.board,e.firstLoad=!1,n(e.opponent,e.playerColor),o(t.partial.isPlayersTurn,t.partial.possibleMove,t.partial.playersTurn),a(t.partial.isPlayersTurn,t.partial.time,t.partial.playersTurn)):0!==t.playersTurn&&t.isPlayersTurn?(e.turnReload&&(Game.Othello.updateBoard(t.board,t.isPlayersTurn,e.playerColor),Game.Othello.highlightChanges(t.board),o(t.isPlayersTurn,t.possibleMove,t.playersTurn),e.turnReload=!1,e.opponentReload=!0),a(t.isPlayersTurn,t.time,t.playersTurn),e.board=t.board):0===t.playersTurn||t.isPlayersTurn?0===t.playersTurn&&t.inGame&&e.lastLoad?(Game.Othello.updateBoard(t.board,t.isPlayersTurn,e.playerColor),Game.Othello.highlightChanges(t.board),o(t.isPlayersTurn,t.possibleMove,t.playersTurn),a(t.isPlayersTurn,t.time,t.playersTurn),e.board=t.board,e.winner=t.winner,e.lastLoad=!1):t.inGame||e.endLoad||(e.lastLoad&&(Game.Othello.updateBoard(e.board,!1,e.playerColor),o(t.isPlayersTurn,t.possibleMove,t.playersTurn),a(t.isPlayersTurn,t.time,t.playersTurn),e.winner=1==e.playerColor?2:1,e.lastLoad=!1),document.getElementById("game-status").textContent=0===e.winner?"Drew":e.winner===e.playerColor?"Won":"Lost",document.getElementById("opponent-name").textContent=e.opponent,e.endLoad=!0):(e.turnReload||(Game.Othello.updateBoard(t.board,t.isPlayersTurn,e.playerColor),Game.Othello.makeMove(t.board),o(t.isPlayersTurn,t.possibleMove,t.playersTurn),e.turnReload=!0),a(t.isPlayersTurn,t.time,t.playersTurn),e.board=t.board);return t}).catch(function(e){return console.log(e.message),{data:null,error:e.message}})},n=function(e,t){var n=document.getElementById("player-info");document.getElementById("opponent-name").textContent=e,n.classList.add("flex","fade-in");var a=document.getElementById("player-color-indicator"),o=document.getElementById("opponent-color-indicator");a.classList.remove("white","black"),o.classList.remove("white","black"),1===t?(a.classList.add("white"),o.classList.add("black")):(a.classList.add("black"),o.classList.add("white")),document.getElementById("score-display").classList.add("flex","fade-in"),document.getElementById("player-score").id=1===t?"white-score":"black-score",document.getElementById("opponent-score").id=1===t?"black-score":"white-score"},a=function(e,t,n){var a=document.getElementById("timer-color-indicator");0!==n?(e?(a.classList.remove("red"),a.classList.add("green"),t.endsWith("0")||t.endsWith("5")||"4"===t||"3"===t||"2"===t||"1"===t?a.classList.add("wobble"):a.classList.remove("wobble")):(a.classList.remove("green"),a.classList.add("red")),document.getElementById("time-remaining").textContent=t):(a.classList.remove("green","red"),a.classList.add("hidden"))},o=function(e,t,n){var a=document.getElementById("feedback-widget"),o=document.getElementById("button-container"),r=document.getElementById("pass-button"),i=document.getElementById("forfeit-button");if(0===n){var s=document.getElementById("rematch-button");o.classList.add("flex"),o.classList.remove("hidden"),r.classList.add("hidden"),r.classList.remove("inline-block","fade-in-wobble"),i.classList.add("hidden"),i.classList.remove("inline-block","fade-in"),s.classList.add("inline-block","fade-in"),s.classList.remove("hidden")}else e?(a.classList.add("visible"),a.classList.remove("hidden"),o.classList.add("flex"),o.classList.remove("hidden"),t?(r.classList.add("hidden"),r.classList.remove("inline-block")):(r.classList.add("inline-block","fade-in-wobble"),r.classList.remove("hidden")),i.classList.add("inline-block","fade-in"),i.classList.remove("hidden")):(a.classList.add("hidden"),a.classList.remove("visible"),o.classList.add("hidden"),o.classList.remove("flex"),r.classList.add("hidden"),r.classList.remove("inline-block","fade-in-wobble"),i.classList.add("hidden"),i.classList.remove("inline-block","fade-in"))},r=function(){return e.board},i=function(){return e.opponent};return{getGameState:function(){return t()},getBoard:function(){return r()},getOpponent:function(){return i()}}}(),Game.Othello=function(){var e=function(){var e=document.getElementById("game-board-container");e.innerHTML="",e.classList.add("game-board-container");var t=document.createElement("table");t.className="othello-board";for(var n=0;n<8;n++){for(var a=document.createElement("tr"),o=0;o<8;o++){var r=document.createElement("td");r.dataset.row=n,r.dataset.col=o,r.classList.add("board-cell",(n+o)%2==0?"even":"odd","distort");var i=Math.random(),s=Math.random(),l=Math.random(),c=.02*(8*n+o);r.style.setProperty("--random-x",i),r.style.setProperty("--random-y",s),r.style.setProperty("--random-rot",l),r.style.setProperty("--animation-delay","".concat(c,"s")),a.appendChild(r)}t.appendChild(a)}e.appendChild(t)},t=function(e,t,a){var o=document.getElementById("game-board-container");o.innerHTML="",o.classList.remove("game-board-container");var r=document.createElement("table");r.className="othello-board";for(var i=0;i<8;i++){for(var s=document.createElement("tr"),l=0;l<8;l++){var c=document.createElement("td");c.dataset.row=i,c.dataset.col=l,c.classList.add("board-cell"),c.classList.add((i+l)%2==0?"even":"odd");var d=e[i][l],u=document.createElement("div");if(u.classList.add("cell-div"),1===d){var m=document.createElement("i");m.className="fa fa-circle white-piece",u.appendChild(m)}else if(2===d){var p=document.createElement("i");p.className="fa fa-circle black-piece",u.appendChild(p)}else if(3===d&&t){var f=document.createElement("div");f.className="possible-move",f.classList.add(1===a?"white-border":"black-border"),u.appendChild(f)}c.appendChild(u),s.appendChild(c)}r.appendChild(s)}o.appendChild(r),n()},n=function(){var e=0,t=0;document.querySelectorAll("#game-board-container td").forEach(function(n){var a=n.querySelector("i");a&&(a.classList.contains("white-piece")?e++:a.classList.contains("black-piece")&&t++)}),document.getElementById("white-score").textContent=e,document.getElementById("black-score").textContent=t},a=function(e){var t=Game.Model.getBoard()||[];document.querySelectorAll("#game-board-container td").forEach(function(n){var a,o=parseInt(n.dataset.row,10),r=parseInt(n.dataset.col,10),i=e[o][r];if(i!==((null===(a=t[o])||void 0===a?void 0:a[r])||0)&&(1===i||2===i)){var s=n.querySelector("i");s&&(s.classList.remove("highlight"),s.classList.add("glow"))}})},o=function(e){var t=Game.Model.getBoard()||[];document.querySelectorAll("#game-board-container td").forEach(function(n){var a,o=parseInt(n.dataset.row,10),r=parseInt(n.dataset.col,10),i=e[o][r];if(i!==((null===(a=t[o])||void 0===a?void 0:a[r])||0)&&(1===i||2===i)){var s=n.querySelector("i");s&&(s.classList.remove("glow"),s.classList.add("highlight"))}})};return{board:function(){return e()},updateBoard:function(e,n,a){t(e,n,a)},makeMove:function(e){a(e)},highlightChanges:function(e){o(e)}}}();