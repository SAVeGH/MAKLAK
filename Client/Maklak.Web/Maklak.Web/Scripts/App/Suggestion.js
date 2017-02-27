function Suggest() { }

    Suggest.currentInput;
    
    Suggest.lockInput = false;
    Suggest.ctrlPressed = false;

    Suggest.HideSuggestion = function (e) {


        var suggestElement = $("#suggestiontPopUp");
        var se = document.getElementById("suggestiontPopUp")

        if (suggestElement.css("display") == "none")
            return;

        var clientRect = se.getBoundingClientRect();
        var inputRect = Suggest.currentInput.getBoundingClientRect();


        if (e) {
            // Если мышь в эелементе ввода или над окном подсказки - выход
            // event.clientX, event.clientY приходят из события $(document).click. Для события onblur они имеют значение "undefined"        
            if (Suggest.pointInRect(e.clientX, e.clientY, clientRect) || Suggest.pointInRect(e.clientX, e.clientY, inputRect))
                return;
        }        

        suggestElement.hide();       

        Suggest.currentInput = null;
        

    }

    Suggest.pointInRect = function (x, y, rect) {
        if ((x >= rect.left && x <= rect.right) && (y >= rect.top && y <= rect.bottom))
            return true;

        return false;
    }

    Suggest.ShowSuggestion = function (e,inputElement) {
        
       

        Suggest.currentInput = inputElement;
        

        var suggestElement = $("#suggestiontPopUp");
        var suggestContentElement = $("#suggestionContent");
        var inputID = "#" + inputElement.id;
        var input = $(inputID);
        if (suggestElement.css("display") == "none") {

            var clientRect = inputElement.getBoundingClientRect();
            var scrl = Suggest.defScroll();
            suggestElement.css('left', clientRect.left + scrl.x);
            suggestElement.css('top', clientRect.bottom + scrl.y);
            //suggestElement.height(inputElement.clientHeight * 7);
            suggestElement.width(inputElement.clientWidth);

        }

        function fillSuggestion(data) {

            if (data == '') {
                Suggest.HideSuggestion(null);
                return;
            }

            suggestContentElement.html(data);
            suggestElement.show();
        }

        var url = GetURL() + '/Suggestion/MakeSuggestion';

        var suggestionKey = input.attr('data-suggestionKey');
        
        var valueEntered = input.val();        
        var sidValue = $("#SID").val();

        var formValue = 'InputValue=' + valueEntered + '&SuggestionKey=' + suggestionKey + '&SID=' + sidValue;
        
        $.post(url, formValue, fillSuggestion);


    }    

    Suggest.SetSuggestion = function (itemId,suggestionValue) {
        
        
        Suggest.currentInput.value = suggestionValue;

        $(Suggest.currentInput).data("itemId", itemId);
        
        Suggest.HideSuggestion();

        
    }

    Suggest.AddItem = function (inputId, suggestionKey) {

        var inputElement = $('#' + inputId);
        var itemId = inputElement.data('itemId');

        inputElement.val('');
        inputElement.data("itemId", 0);

        var url = GetURL() + '/Suggestion/AddItem';

        var sidValue = $("#SID").val();

        var formValue = 'ItemId=' + itemId + '&SuggestionKey=' + suggestionKey + '&SID=' + sidValue;

        $.post(url, formValue);

    }

    Suggest.defScroll = function () {
        var x = y = 0;
        // Gecko поддерживает свойства scrollX(scrollY)
        // Для IE & Opera приходится идти в обход
        x = (window.scrollX) ? window.scrollX : document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft;
        y = y = (window.scrollY) ? window.scrollY : document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop;
        return { x: x, y: y };
    }

    // Делается подписка на событие click документа для закрытия PopUp окон (Suggestion,Calendar...)  т.к. элемент div не имеет события onblur
    
    $(document).click(Suggest.HideSuggestion)
