function verificaParole() {
     var parola1 = document.getElementById('password').value;
     var parola2 = document.getElementById('repeat-password').value;
     var errorMessageMatch = document.getElementById('error-message-match');
     var errorMessageLength = document.getElementById('error-message-length');
     var errorMessagePolicy = document.getElementById('error-message-policy');
     var submitButton = document.getElementById('submit-button');

     // Inițial presupunem că totul este în regulă și activăm butonul
     var valid = true;
     var policyRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;

     errorMessageLength.style.display = 'none';
     errorMessageMatch.style.display = 'none';
     errorMessagePolicy.style.display = 'none';

     if (parola1 !== parola2)
     {
          errorMessageMatch.style.display = 'block';
          valid = false;
     }
     else
     {
          errorMessageMatch.style.display = 'none';
          if (parola1.length < 8 || parola2.length < 8)
          {
               errorMessageLength.style.display = 'block';
               valid = false;
          }
          else
          {
               errorMessageLength.style.display = 'none';
               if (!policyRegex.test(parola1) || !policyRegex.test(parola2)) {
                    errorMessagePolicy.style.display = 'block';
                    valid = false;
               }
               else {
                    errorMessagePolicy.style.display = 'none';
               }
          }
     }
     submitButton.disabled = !valid;
}