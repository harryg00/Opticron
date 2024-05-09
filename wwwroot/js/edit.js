// This file is for the edit page, and controls the modals / link between database and controller functions
function getCard(id) {
    // Gets the card url if "getCard" is called (see below for click listener)
    var call = $.ajax({
        url: getCardUrl,
        data: {
            cardId: id // sends the id (so card can be fetched using id)
        },
        dataType: 'json'
    });

    // If it works
    call.done(function (card) {
        // Take the information
        var id = card.id;
        var title = card.title;
        var description = card.description;
        var file = card.filePath;
        var link = card.link;

        // Set the values of the modal to be what is in the database
        $('#idHidden').val(id);
        $('#title').val(title);
        $('#description').val(description);
        $('#file').val(file);
        $('#link').val(link);
    });

    // If it fails, log the error in the browser console
    call.fail(function (error) {
        console.log(error)
    });
};

// This is a listener for a mouse click on the edit-card button (pencil on the Edit page)
// This calls getCard so the information is linked
$('body').on('click', '.edit-card-button', function () {
    var cardId = $(this).attr('data-cardId');
    getCard(cardId);
});
// This is the secondary listener, which opens the editing modal (pencil on the Edit page)
$('body').on('click', '#pencil', function () {
    var cardId = $(this).parent().attr('data-cardId'); // Gets the card id 
    $('#idHidden').val(cardId); // ensures the card ID is present in HTML
    $('#editingModal').modal('show'); // Show the modal for editing
});

// When any modal is closed (listener)
$('body').on('click', '#modalClose', function () {
    // Takes the values from the modal
    var cardId = $('#idHidden').val();
    var title = $('#title').val();
    var description = $('#description').val();
    var file = $('#file').val();
    var link = $('#link').val();

    // Stores as a JSON object
    var data = {
        cardId: cardId,
        title: title,
        description: description,
        file: file,
        link: link
    };

    // Makes a call to updateCard in HomeController (updates the database with the new information uploaded by user)
    $.ajax({
        url: updateCard,
        method: 'POST',
        data: data,
        // error logging
        success: function (response) {
            console.log('Card updated successfully');
        },
        error: function (xhr, status, error) {
            console.error('Error updating card:', error);
        }
    });

    $('#editingModal').modal('hide'); // remove the modal
    alert("Refresh the page to see changes"); // location.refresh() sometimes causes an error - the user is instead asked to refresh manually
});

// When the user clicks on the delete button (inside the modal) (listener)
$('body').on('click', '#addModalDataDelete', function () {
    var cardId = $('#idHidden').val(); // takes the id of the card

    var data = {
        cardId: cardId
    };

    // calls the DeleteCard function in HomeController (deletes the card from the database)
    $.ajax({
        url: deleteCard,
        method: 'POST',
        data: data,
        // Error logging
        success: function (response) {
            console.log('Card updated successfully');
        },
        error: function (xhr, status, error) {
            console.error('Error updating card:', error);
        }
    });

    $('#editingModal').modal('hide'); // close the modal
    alert("Refresh the page to see changes"); // location.refresh() sometimes causes an error - the user is instead asked to refresh manually
});

// When the add button is clicked, show the modal to add a card
$('body').on('click', '#addButton', function () {
    $('#newModal').modal('show');
});

// When the addModal is closed, take the information and upload it into the database
$('body').on('click', '#addModalClose', function () {
    // Takes the infomrmation from the input tags
    var title = $('#newTitle').val();
    var description = $('#newDescription').val();
    var file = $('#newFile').val();
    var link = $('#newLink').val();
    var category = $('#category').val();

    // Turns into a JSON object to be sent
    var data = {
        newTitle: title,
        newDescription: description,
        newFile: file,
        newLink: link,
        category: category
    };
    // Makes an ajax call to the NewCard function in HomeController
    $.ajax({
        url: newCard,
        method: 'POST',
        data: data,
        // Error logging
        success: function (response) {
            console.log('Card updated successfully');
        },
        error: function (xhr, status, error) {
            console.error('Error updating card:', error);
        }
    });
    $('#newModal').modal('hide'); // Remove the modal 
    alert("Refresh the page to see changes"); // location.refresh() sometimes causes an error - the user is instead asked to refresh manually
});

// When the user requests to reset the data to default values (listener)
$('body').on('click', '#resetButton', function () {
    // Makes an ajax call to ResetDatabase in HomeController 
    // does not need any data as default information is stored in external SQL query file
    $.ajax({
        url: resetDatabase,
        method: 'POST',
        //error logging
        success: function (response) {
            console.log('Database reset to Defaults')
        },
        error: function (xhr, status, error) {
            console.error('Error resetting database:', error);
        }
    });
    alert("Refresh the page to see changes"); // location.refresh() sometimes causes an error - the user is instead asked to refresh manually
});