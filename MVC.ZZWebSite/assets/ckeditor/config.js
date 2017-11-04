/**
 * @license Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here.
	// For the complete reference:
	// http://docs.ckeditor.com/#!/api/CKEDITOR.config

    // The toolbar groups arrangement, optimized for two toolbar rows.
    config.filebrowserBrowseUrl = '../ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '../ckfinder/ckfinder.html?type=Images';
    config.filebrowserFlashBrowseUrl = '../ckfinder/ckfinder.html?type=Flash';
    config.filebrowserUploadUrl = '../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    config.language = 'zh-cn';
    config.toolbar =
	[
	['Source','-','Preview','-','Templates'],
	['PasteText', 'PasteFromWord', 'Autoformat'],
	['Find','Replace','-','SelectAll','RemoveFormat'],
	//['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'],
	'/',
	['Bold','Italic','Underline','Strike','-','Subscript','Superscript'],
	['NumberedList','BulletedList','-','Outdent','Indent','Blockquote'],
	['JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock'],
	['Link','Unlink','Anchor'],
	['Image','Table','HorizontalRule','SpecialChar','PageBreak'],
	'/',
	['Styles', 'Format', 'Font', 'FontSize', 'lineheight'],
	['TextColor','BGColor'],
	['Maximize']
	];
    config.font_names = 'ËÎÌå/ËÎÌå;ºÚÌå/ºÚÌå;·ÂËÎ/·ÂËÎ_GB2312;¿¬Ìå/¿¬Ìå_GB2312;Á¥Êé/Á¥Êé;Ó×Ô²/Ó×Ô²;ÑÅºÚ/ÑÅºÚ;' + config.font_names;
    //config.extraPlugins += (config.extraPlugins ? ',autoformat,lineheight' : 'autoformat,lineheight');
    //config.toolbarGroups = [
		
    //{ name: 'document', groups: ['mode', 'document', 'doctools'] },
    //{ name: 'clipboard', groups: ['clipboard', 'undo' ] },
    ////{ name: 'editing', groups: [ 'find', 'selection', 'spellchecker' ] },
    //{ name: 'links' },
    //{ name: 'insert' },
    ////{ name: 'forms' },
    //{ name: 'tools' },
    ////{ name: 'others' },
    //{ name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
    //{ name: 'paragraph',   groups: [ 'list', 'indent', 'blocks', 'align' ] },
    //{ name: 'styles' },
    //{ name: 'colors' }//,
    //{ name: 'about' }
    //];
	// Remove some buttons, provided by the standard plugins, which we don't
	// need to have in the Standard(s) toolbar.
	config.removeButtons = 'Underline,Subscript,Superscript';
};
