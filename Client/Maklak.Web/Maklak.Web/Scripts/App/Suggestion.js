function Suggest() { }

    Suggest.currentInput;
    Suggest.currentInputValue;
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
        
        var lastText = Suggest.currentInputValue.textvalue;

        if (Suggest.currentInputValue.value == '')
            Suggest.currentInput.value = ''; // ничего небыло выбрано из списка
        else
            Suggest.currentInput.value = lastText; // если ничего нового не выбрано - вернуть текст предыдущего выбора

        Suggest.currentInput = null;
        Suggest.currentInputValue = null;

    }

    Suggest.pointInRect = function (x, y, rect) {
        if ((x >= rect.left && x <= rect.right) && (y >= rect.top && y <= rect.bottom))
            return true;

        return false;
    }

    Suggest.ShowSuggestion = function (e,inputElement,valueElement) {
        
        if (e.ctrlKey || e.keyCode == 17) // нажат ctrl
            return;

        Suggest.currentInput = inputElement;
        Suggest.currentInputValue = valueElement;

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

        var suggestionKey = input.attr('suggestionKey');
        var valueEntered = input.val();

        if (valueEntered == '') {
            Suggest.HideSuggestion(null); // не открывать подсказку при пустом вводе
            return;
        }

        var formValue = 'InputValue=' + valueEntered + '&suggestionKey=' + suggestionKey;
        
        $.post(url, formValue, fillSuggestion);


    }

    

    Suggest.CheckInput = function (inputElement, valueElement) {
        if (Suggest.lockInput)
            return;        

        if (inputElement.value != '')
            return; // если не пустой ввод - не затирать id предыдущего выбора

        // при пустом вводе символов очищаем valueElement. Он устанавливается только при выборе из списка.
        valueElement.value = "";

    }    

    Suggest.SetSuggestion = function (suggestionKey,suggestionValue) {
        
        Suggest.lockInput = true; // блокируем стирание выбранного значения
        Suggest.currentInput.value = suggestionValue;
        Suggest.currentInputValue.value = suggestionKey; // устанавливаем выбор
        Suggest.currentInputValue.textvalue = suggestionValue; // последнее выбранное значение
        
        Suggest.lockInput = false;
        Suggest.HideSuggestion();
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
