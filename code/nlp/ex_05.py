from sklearn.datasets import fetch_20newsgroups
from sklearn.naive_bayes import MultinomialNB
from sklearn.feature_extraction.text import TfidfTransformer
from sklearn.feature_extraction.text import CountVectorizer

# Определение карты категорий
category_map = {'talk.politics.misc': 'Politics',
    'rec.autos': 'Autos', 'rec.sport.hockey': 'Hockey',
    'sci.electronics': 'Electronics', 'sci.med': 'Medicine'}

# Получение тренировочного набора данных
training_data = fetch_20newsgroups(subset='train',
    categories=category_map.keys(), shuffle=True,
    random_state=5)

# Создание векторизатора и извлечение счетчиков слов
count_vectorizer = CountVectorizer()
train_tc = count_vectorizer.fit_transform(training_data.data)
print("\nDimensions of training data:", train_tc.shape)

# Создание преобразователя tf-idf
tfidf = TfidfTransformer()
train_tfidf = tfidf.fit_transform(train_tc)

input_data = [
    'You need to be careful with cars when you are driving on slippery roads', 
    'A lot of devices can be operated wirelessly',
    'Players need to be careful when they are close to goal posts',
    'Political debates help us understand the perspectives of both sides'
]

# Обучение мультиномиального байесовского классификатора
classifier = MultinomialNB().fit(train_tfidf, training_data.target)

# Преобразование входных данных с помощью
# векторизатора счетчиков
input_tc = count_vectorizer.transform(input_data)

# Преобразование векторизованных данных с помощью
# преобразователя tf-idf
input_tfidf = tfidf.transform(input_tc)

# Прогнозирование результирующих категорий
predictions = classifier.predict(input_tfidf)

# Прогнозирование результирующих категорий
for sent, category in zip(input_data, predictions):
    print('\nInput:', sent, '\nPredicted category:', \
            category_map[training_data.target_names[category]])
