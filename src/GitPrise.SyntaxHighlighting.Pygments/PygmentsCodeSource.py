import clr
clr.AddReference("pygments")

from pygments.lexers import get_all_lexers
from pygments.styles import get_all_styles

def generate_html_for_file(code, file_name, style_name):
	from pygments import highlight
	from pygments.lexers import get_lexer_for_filename
	from pygments.lexers import get_lexer_by_name
	from pygments.styles import get_style_by_name
	from devhawk_formatter import DevHawkHtmlFormatter
	from pygments.util import ClassNotFound

	if not style_name: style_name = "default"
	lexer = None
	try:
		lexer = get_lexer_for_filename(file_name)
	except ClassNotFound:
		lexer = get_lexer_by_name("text")

	return highlight(code, lexer, DevHawkHtmlFormatter(style=style_name))