from nltk.stem import WordNetLemmatizer

input_words = ['writing', 'calves', 'Ье', 'branded', 'horse',
        'randomize', 'possiЬly', 'provision', 'hospital',
        'kept', 'scratchy', 'code']

# Создание объекта лемматизатора
lemmatizer = WordNetLemmatizer()

# Создание списка имен лемматизаторов для их отображения
lemmatizer_names = ['NOUN LEMМATIZER', 'VERB LEММATIZER']
formatted_text = '{:>24}' * (len(lemmatizer_names) + 1)
print('\n', formatted_text.format('INPUT WORD', *lemmatizer_names), '\n', '='*75)

# Лемматизация слов и отображение результатов
for word in input_words:
    output = [word, lemmatizer.lemmatize(word, pos='n'),
        lemmatizer.lemmatize(word, pos='v')]
    print(formatted_text.format(*output))

