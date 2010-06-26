// ******************************
// add console stubs for browsers / systems without support
// ******************************
if (!window.console) console = {};
console.log = console.log || function() { };
console.warn = console.warn || function() { };
console.error = console.error || function() { };
console.info = console.info || function() { };

// ******************************
// common global functions
// ******************************
function isNull(o, alt) {
	return o == null ? alt : o;
};

// ******************************
// uiUtils
// ******************************
uiUtils = {
	formatDate: function(toFormat) {
		var date = $.datepicker.formatDate('DD dd MM', toFormat);
		var time = toFormat.toLocaleTimeString();
		return date + ' ' + time;
	},

	enableButton: function(button, enabled) {
		button.button(enabled ? 'enable' : 'disable');
	}
};

// ******************************
// stringUtils
// ******************************
stringUtils = {
	format: function(template)	{
	  for(i = 1; i < arguments.length; i++)	  {
		template = template.replace('{' + (i - 1) + '}', arguments[i]);
	  }
	  return template;
	}
};

// ******************************
// dateUtils
// ******************************
dateUtils = {
	areEqual: function(first, second) {
		return first.toDateString() == second.toDateString();
	}
};