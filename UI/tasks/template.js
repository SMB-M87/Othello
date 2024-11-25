import gulp from "gulp";
const { src, dest } = gulp;

import handlebars from "gulp-handlebars";
import wrap from "gulp-wrap";
import declare from "gulp-declare";
import concat from "gulp-concat";
import merge from "merge-stream";
import path from "path";

const template = function (backendPath, templates, partials) {
  return function () {
    const parts = src(partials)
      .pipe(handlebars())
      .pipe(
        wrap(
          'this["spa_templates"] = this["spa_templates"] || {}; ' +
          'this["spa_templates"][<%= processPartialName(file.relative) %>] = Handlebars.template(<%= contents %>); ' +
          'Handlebars.registerPartial(<%= processPartialName(file.relative) %>, this["spa_templates"][<%= processPartialName(file.relative) %>]);',
          {},
          {
            imports: {
              processPartialName: function (fileName) {
                return JSON.stringify(
                  path.basename(fileName, ".js").substr(1)
                );
              },
            },
          }
        )
      );

    const temps = src(templates)
      .pipe(handlebars())
      .pipe(wrap("Handlebars.template(<%= contents %>)"))
      .pipe(
        declare({
          namespace: "spa_templates",
          noRedeclare: true,
          processName: function (filePath) {
            return declare.processNameByPath(
              filePath.replace("src\\template\\", "")
            );
          },
        })
      );

    return merge(parts, temps)
      .pipe(concat("template.js"))
      .pipe(dest(backendPath + "/js"));
  };
};

export default { template };
