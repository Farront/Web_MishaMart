async function translateText(text, targetLanguage) {
    const apiKey = "AIzaSyDRdSlvoM5VQqbyDNDWNs6sxLUsAJCo79E"; 
    const url = `https://translation.googleapis.com/language/translate/v2?key=${apiKey}`;

    const response = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            q: text,
            target: targetLanguage
        })
    });

    const data = await response.json();
    return data.data.translations[0].translatedText;
}

