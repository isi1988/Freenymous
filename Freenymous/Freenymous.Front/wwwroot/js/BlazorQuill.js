(function () {
    window.QuillFunctions = {
        createQuill: function (quillElementId) {
            let quillElement = document.getElementById(quillElementId);
            var options = {
                debug: 'info',
                modules: {
                    toolbar: '#toolbar'
                },
                placeholder: 'Compose an epic...',
                readOnly: false,
                theme: 'snow'
            };
            // set quill at the object we can call
            // methods on later
            new Quill(quillElement, options);
        },
        getQuillContent: function (quillElementId) {
            let quillElement = document.getElementById(quillElementId);
            return JSON.stringify(quillElement.__quill.getContents());
        },
        getQuillText: function (quillElementId) {
            let quillElement = document.getElementById(quillElementId);
            return quillElement.__quill.getText();
        },
        getQuillHTML: function (quillElementId) {
            let quillElement = document.getElementById(quillElementId);
            return quillElement.__quill.root.innerHTML;
        },
        loadQuillContent: function (quillElementId, quillContent) {
            let quillElement = document.getElementById(quillElementId);
            return quillElement.__quill.setContents(quillContent, 'api');
        },
    };
})();