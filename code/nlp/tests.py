from sklearn.datasets import load_files

# load_files возвращает коллекцию, содержащую обучающие тексты и обучающие метки
reviews_train = load_files(r"c:/data/aclImdb/train/")

text_train, y_train = reviews_train.data, reviews_train.target

print("тип text_train: {}".format(type(text_train)))
print("длина text_train: {}".format(len(text_train)))
print("text_train[1]:\n {}".format(text_train[1]))