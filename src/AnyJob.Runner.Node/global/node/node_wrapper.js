(function () {
    var fs = require('fs');
    var [_, _, entry, input, output] = process.argv;
    var text = fs.readFileSync(input, "utf-8");
    var result = runAction(entry, stripBOM(text));
    fs.writeFileSync(output, JSON.stringify(result));
    function stripBOM(content) {
        if (content.charCodeAt(0) === 0xFEFF) {
            content = content.slice(1);
        }
        return content;
    }
    /**
     * 
     * @param {String} entry
     * @param {String} argsText
     */
    function runAction(entry, argsText) {
        try {
            var args = JSON.parse(argsText);
            var module = require(entry);
            var fun = module.run;
            if (typeof fun !== "function") {
                throw "Can not find run function.";
            }
            var fun_args_define = parseFuncArguments(module.run);
            var fun_args = fun_args_define.map(function (p) { return args[p]; });
            var result = fun.apply(this, fun_args);
            return { 'result': result };
        } catch (error) {
            var errorInfo = {
                message: error.message,
                type: error.name,
                stack: error.stack
            };
            return { 'error': errorInfo };
        }
    }
    /**
     * 
     * @param {Function} fun 
     */
    function parseFuncArguments(fun) {
        var define = fun.toString();
        var startIndex = define.indexOf('(');
        var endIndex = define.indexOf(')', startIndex + 1);
        var argsLine = define.substring(startIndex + 1, endIndex).trim();
        if (!argsLine) return [];
        return argsLine.split(',').map(parseArgument);
    }
    /**
     * 
     * @param {String} argdefine 
     */
    function parseArgument(argdefine) {
        var index = argdefine.indexOf('=');
        return index > 0 ? argdefine.substring(0, index).trim() : argdefine.trim();
    }
})();
