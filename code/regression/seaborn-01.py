import seaborn as sns
import matplotlib.pyplot as plt
import numpy as np
import pandas as pd

df = pd.DataFrame(np.random.randn(300, 4), columns=[f"F{i+1}" for i in range(4)])
df["y"] = np.random.choice([1., 0.], size=len(df))

sns.pairplot(df, vars=df.columns[:-1], hue="y")

plt.show()