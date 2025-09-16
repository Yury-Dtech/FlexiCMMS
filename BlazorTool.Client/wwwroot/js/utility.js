window.BlazorTool = {
    copyToClipboard: async function (text) {
        try {
            await navigator.clipboard.writeText(text);
            console.log('Text copied to clipboard successfully!');
            return true;
        } catch (err) {
            console.error('Could not copy text: ', err);
            return false;
        }
    }
};