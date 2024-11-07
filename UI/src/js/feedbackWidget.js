class FeedbackWidget {
    constructor(elementId) {
        this._elementId = elementId;
    }

    get elementId() { //getter, set keyword voor setter methode
        return this._elementId;
    }

    show(string, element) {
        let alert = "";

        if(element === 'Good Luck'){
            alert = 'alert alert-success';
            $("#feedback-widget").text(element+" - "+string);
            $("#feedback-widget").css("display", "block");
        } else if (element === 'Danger'){
            alert = 'alert alert-danger';
            $("#feedback-widget").text(element+" - "+string);
            $("#feedback-widget").css("display", "block");
        } else {
            alert = 'alert alert-reset';
            $("#feedback-widget").text("");
            $("#feedback-widget").css("display", "none");
            window.alert(element+" - For the fallout...");
        }
        $('#feedback-widget').attr('class', alert);

        if (string && element) {
            this.log({
                message: string,
                type: element
            });
        }
    }

    log(message){
        if(localStorage.getItem("feedback_widget") === null ){
            let store = {
                messages: [message]
            }
            localStorage.setItem('feedback_widget', JSON.stringify(store));
        } else {
            let store = JSON.parse(localStorage.getItem('feedback_widget'));
            store.messages.unshift(message)

            if(store.messages.length > 10) store.messages.pop();

            localStorage.setItem('feedback_widget', JSON.stringify(store))
        }
    }

    removeLog(){
        localStorage.removeItem('feedback_widget');
        console.clear();
    }

    history(){
        let store = JSON.parse(localStorage.getItem('feedback_widget'));

        let string="";
        store.messages.forEach(element => {
            string = string + element.type + " - " + element.message + " \n"
        });

        console.clear();
        console.log(string);
    }
};

$(function () {
    let feedbackWidget = new FeedbackWidget("feedback-widget");

    $("#feedback-widget-succes-button").on("click", function () {
        feedbackWidget.show(document.getElementById("feedback-widget-succes-button").value, document.getElementById("feedback-widget-succes-button").innerText);
        feedbackWidget.history();
    });

    $("#feedback-widget-hide-button").on("click", function () {
        feedbackWidget.show(document.getElementById("feedback-widget-hide-button").value, document.getElementById("feedback-widget-hide-button").innerText);
        feedbackWidget.removeLog();
    });

    $("#feedback-widget-danger-button").on("click", function () {
        feedbackWidget.show(document.getElementById("feedback-widget-danger-button").value, document.getElementById("feedback-widget-danger-button").innerText);
        feedbackWidget.history();
    });
});

