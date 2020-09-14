import seaborn as sns
import matplotlib.pyplot as plt

#увеличим дефолтный размер графиков
# from pylab import rcParams
# rcParams['figure.figsize'] = 8, 5

import pandas as pd

df = pd.read_csv('code/regression/video_games_sales.csv')
df.info()

# df['User_Score'] = df.User_Score.astype('float64')
# df['Year_of_Release'] = df.Year_of_Release.astype('int64')
# df['User_Count'] = df.User_Count.astype('int64')
# df['Critic_Count'] = df.Critic_Count.astype('int64')

df = df.dropna()
print(df.shape)

useful_cols = ['Name', 'Platform', 'Year_of_Release', 'Genre', 
               'Global_Sales', 'Critic_Score', 'Critic_Count',
               'User_Score', 'User_Count', 'Rating'
              ]
df[useful_cols].head()

# sales_df = df[[x for x in df.columns if 'Sales' in x] + ['Year_of_Release']]
# sales_df.groupby('Year_of_Release').sum().plot()

cols = ['Global_Sales', 'Critic_Score', 'Critic_Count', 'User_Score', 'User_Count']
# sns_plot = sns.pairplot(df[cols])
# sns_plot.savefig('pairplot.png')
# sns_plot = sns.distplot(df.Critic_Score)

# top_platforms = df.Platform.value_counts().sort_values(ascending = False).head(5).index.values
# sns.boxplot(y="Platform", x="Critic_Score", data=df[df.Platform.isin(top_platforms)], orient="h")

platform_genre_sales = df.pivot_table(
                        index='Platform', 
                        columns='Genre', 
                        values='Global_Sales', 
                        aggfunc=sum).fillna(0).applymap(float)
sns.heatmap(platform_genre_sales, annot=True, fmt=".1f", linewidths=.5)

plt.show()
