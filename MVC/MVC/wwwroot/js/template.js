this["spa_templates"] = this["spa_templates"] || {}; this["spa_templates"]["board"] = Handlebars.template({"1":function(container,depth0,helpers,partials,data) {
    var stack1, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return "    <tr>\r\n"
    + ((stack1 = lookupProperty(helpers,"each").call(depth0 != null ? depth0 : (container.nullContext || {}),depth0,{"name":"each","hash":{},"fn":container.program(2, data, 0),"inverse":container.noop,"data":data,"loc":{"start":{"line":4,"column":6},"end":{"line":16,"column":15}}})) != null ? stack1 : "")
    + "    </tr>\r\n";
},"2":function(container,depth0,helpers,partials,data) {
    var stack1, helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=container.hooks.helperMissing, alias3="function", alias4=container.escapeExpression, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return "        <td data-row=\""
    + alias4(((helper = (helper = lookupProperty(helpers,"row") || (depth0 != null ? lookupProperty(depth0,"row") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"row","hash":{},"data":data,"loc":{"start":{"line":5,"column":22},"end":{"line":5,"column":29}}}) : helper)))
    + "\" data-col=\""
    + alias4(((helper = (helper = lookupProperty(helpers,"col") || (depth0 != null ? lookupProperty(depth0,"col") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"col","hash":{},"data":data,"loc":{"start":{"line":5,"column":41},"end":{"line":5,"column":48}}}) : helper)))
    + "\" class=\"board-cell "
    + alias4(((helper = (helper = lookupProperty(helpers,"cellClass") || (depth0 != null ? lookupProperty(depth0,"cellClass") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"cellClass","hash":{},"data":data,"loc":{"start":{"line":5,"column":68},"end":{"line":5,"column":81}}}) : helper)))
    + " distort\"\r\n            style=\"--random-x: "
    + alias4(((helper = (helper = lookupProperty(helpers,"randomX") || (depth0 != null ? lookupProperty(depth0,"randomX") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"randomX","hash":{},"data":data,"loc":{"start":{"line":6,"column":31},"end":{"line":6,"column":42}}}) : helper)))
    + "; --random-y: "
    + alias4(((helper = (helper = lookupProperty(helpers,"randomY") || (depth0 != null ? lookupProperty(depth0,"randomY") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"randomY","hash":{},"data":data,"loc":{"start":{"line":6,"column":56},"end":{"line":6,"column":67}}}) : helper)))
    + "; --random-rot: "
    + alias4(((helper = (helper = lookupProperty(helpers,"randomRot") || (depth0 != null ? lookupProperty(depth0,"randomRot") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"randomRot","hash":{},"data":data,"loc":{"start":{"line":6,"column":83},"end":{"line":6,"column":96}}}) : helper)))
    + "; --animation-delay: "
    + alias4(((helper = (helper = lookupProperty(helpers,"animationDelay") || (depth0 != null ? lookupProperty(depth0,"animationDelay") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"animationDelay","hash":{},"data":data,"loc":{"start":{"line":6,"column":117},"end":{"line":6,"column":135}}}) : helper)))
    + "s;\">\r\n"
    + ((stack1 = container.invokePartial(lookupProperty(partials,"pawn"),depth0,{"name":"pawn","hash":{"flip":(depth0 != null ? lookupProperty(depth0,"flip") : depth0),"highlight":(depth0 != null ? lookupProperty(depth0,"highlight") : depth0),"playerColorClass":(depth0 != null ? lookupProperty(depth0,"playerColorClass") : depth0),"pieceColorClass":(depth0 != null ? lookupProperty(depth0,"pieceColorClass") : depth0),"hasPiece":(depth0 != null ? lookupProperty(depth0,"hasPiece") : depth0),"isPossibleMove":(depth0 != null ? lookupProperty(depth0,"isPossibleMove") : depth0)},"data":data,"indent":"          ","helpers":helpers,"partials":partials,"decorators":container.decorators})) != null ? stack1 : "")
    + "        </td>\r\n";
},"compiler":[8,">= 4.3.0"],"main":function(container,depth0,helpers,partials,data) {
    var stack1, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return "<table class=\"othello-board\">\r\n"
    + ((stack1 = lookupProperty(helpers,"each").call(depth0 != null ? depth0 : (container.nullContext || {}),(depth0 != null ? lookupProperty(depth0,"boardRows") : depth0),{"name":"each","hash":{},"fn":container.program(1, data, 0),"inverse":container.noop,"data":data,"loc":{"start":{"line":2,"column":2},"end":{"line":18,"column":11}}})) != null ? stack1 : "")
    + "</table>";
},"usePartial":true,"useData":true});
this["spa_templates"] = this["spa_templates"] || {};
this["spa_templates"]["body"] = Handlebars.template({"compiler":[8,">= 4.3.0"],"main":function(container,depth0,helpers,partials,data) {
    var stack1, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return "<section id=\"player-info\" class=\"player-info\">\r\n  <h2>\r\n    <span id=\"game-status\">Playing</span> against\r\n    <span id=\"opponent-name\">...</span>\r\n    <span id=\"forfeit-title\"></span>\r\n  </h2>\r\n</section>\r\n\r\n<section id=\"score-display\" class=\"score-display\">\r\n  <p>\r\n    <span id=\"player-color-indicator\" class=\"color-indicator\">\r\n      <strong><span id=\"player-score\">2</span></strong>\r\n    </span>\r\n  </p>\r\n  <p>\r\n    <span id=\"timer-color-indicator\" class=\"color-indicator\">\r\n      <strong><span id=\"time-remaining\">30</span></strong>\r\n    </span>\r\n  </p>\r\n  <p>\r\n    <span id=\"opponent-color-indicator\" class=\"color-indicator\">\r\n      <strong><span id=\"opponent-score\">2</span></strong>\r\n    </span>\r\n  </p>\r\n</section>\r\n\r\n<section id=\"game-board-container\">\r\n"
    + ((stack1 = container.invokePartial(lookupProperty(partials,"board"),depth0,{"name":"board","hash":{"boardRows":(depth0 != null ? lookupProperty(depth0,"boardData") : depth0)},"data":data,"indent":"  ","helpers":helpers,"partials":partials,"decorators":container.decorators})) != null ? stack1 : "")
    + "</section>\r\n\r\n<section id=\"button-container\">\r\n  <button id=\"pass-button\" class=\"button button--succes\">Pass</button>\r\n  <button id=\"forfeit-button\" class=\"button button--danger\">Forfeit</button>\r\n  <button id=\"rematch-button\" class=\"button button--info hidden\"><i class=\"fas fa-redo\"></i><span>Rematch</span></button>\r\n</section>\r\n\r\n<section id=\"feedback-section\" aria-label=\"Feedback Widget\">\r\n  <article id=\"feedback-widget\" role=\"alert\"></article>\r\n</section>";
},"usePartial":true,"useData":true});
this["spa_templates"] = this["spa_templates"] || {}; this["spa_templates"]["pawn"] = Handlebars.template({"1":function(container,depth0,helpers,partials,data) {
    var helper, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return "    <div class=\"possible-move "
    + container.escapeExpression(((helper = (helper = lookupProperty(helpers,"playerColorClass") || (depth0 != null ? lookupProperty(depth0,"playerColorClass") : depth0)) != null ? helper : container.hooks.helperMissing),(typeof helper === "function" ? helper.call(depth0 != null ? depth0 : (container.nullContext || {}),{"name":"playerColorClass","hash":{},"data":data,"loc":{"start":{"line":3,"column":30},"end":{"line":3,"column":50}}}) : helper)))
    + "-border\"></div>\r\n";
},"3":function(container,depth0,helpers,partials,data) {
    var stack1, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return ((stack1 = lookupProperty(helpers,"if").call(depth0 != null ? depth0 : (container.nullContext || {}),(depth0 != null ? lookupProperty(depth0,"hasPiece") : depth0),{"name":"if","hash":{},"fn":container.program(4, data, 0),"inverse":container.noop,"data":data,"loc":{"start":{"line":5,"column":4},"end":{"line":7,"column":11}}})) != null ? stack1 : "");
},"4":function(container,depth0,helpers,partials,data) {
    var stack1, helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return "      <i class=\"fa fa-circle "
    + container.escapeExpression(((helper = (helper = lookupProperty(helpers,"pieceColorClass") || (depth0 != null ? lookupProperty(depth0,"pieceColorClass") : depth0)) != null ? helper : container.hooks.helperMissing),(typeof helper === "function" ? helper.call(alias1,{"name":"pieceColorClass","hash":{},"data":data,"loc":{"start":{"line":6,"column":29},"end":{"line":6,"column":48}}}) : helper)))
    + " "
    + ((stack1 = lookupProperty(helpers,"if").call(alias1,(depth0 != null ? lookupProperty(depth0,"highlight") : depth0),{"name":"if","hash":{},"fn":container.program(5, data, 0),"inverse":container.noop,"data":data,"loc":{"start":{"line":6,"column":49},"end":{"line":6,"column":82}}})) != null ? stack1 : "")
    + " "
    + ((stack1 = lookupProperty(helpers,"if").call(alias1,(depth0 != null ? lookupProperty(depth0,"flip") : depth0),{"name":"if","hash":{},"fn":container.program(7, data, 0),"inverse":container.noop,"data":data,"loc":{"start":{"line":6,"column":83},"end":{"line":6,"column":106}}})) != null ? stack1 : "")
    + "\"></i>\r\n";
},"5":function(container,depth0,helpers,partials,data) {
    return "highlight";
},"7":function(container,depth0,helpers,partials,data) {
    return "flip";
},"compiler":[8,">= 4.3.0"],"main":function(container,depth0,helpers,partials,data) {
    var stack1, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return "<div class=\"cell-div\">\r\n"
    + ((stack1 = lookupProperty(helpers,"if").call(depth0 != null ? depth0 : (container.nullContext || {}),(depth0 != null ? lookupProperty(depth0,"isPossibleMove") : depth0),{"name":"if","hash":{},"fn":container.program(1, data, 0),"inverse":container.program(3, data, 0),"data":data,"loc":{"start":{"line":2,"column":2},"end":{"line":8,"column":9}}})) != null ? stack1 : "")
    + "</div>";
},"useData":true});