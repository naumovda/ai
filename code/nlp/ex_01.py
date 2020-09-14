from nltk.tokenize import sent_tokenize, word_tokenize, WordPunctTokenizer

# Определение входного текста
input_text = "Do you know how tokenization works? It's actually \
    quite interesting! Let's analyze а couple of sentences and \
    figure it out."

# Токенизация предложений
print("Sentence tokenizer:")
print(sent_tokenize(input_text))

# Токенизатор слов
print("Word tokenizer:")
print(word_tokenize(input_text))

