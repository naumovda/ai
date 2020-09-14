import numpy as np
from nltk.corpus import brown

# Разбиение входного текста на блоки,
# причем каждый блок содержит N слов
def chunker(input_data, N):
    input_words = input_data.split(' ')
    output = []

    count, cur_chunk = 0, []
    for word in input_words:
        cur_chunk.append(word)
        count += 1
        if count == N:
            output.append(' '.join(cur_chunk))
            count, cur_chunk = 0, []

    output.append(' '.join(cur_chunk))

    return output
    
if __name__ =='__main__':
    # Чтение первых 12000 слов из коллекции Brown
    input_data = ' '.join(brown.words() [:12000])

    # Определение количества слов в каждом блоке
    chunk_size = 700

    chunks = chunker(input_data, chunk_size)

    print('\nNumЬer of text chunks =', len(chunks), '\n')
    
    for i, chunk in enumerate(chunks):
        print('Chunk', i+1, '==>', chunk[:50])