import plotly.express as px
import pandas as pd

df = pd.read_csv("usa_states_colors.csv")
colors = {"a": "blue", "b": "red", "c": "green"}
fig = px.choropleth(df,  # input dataframe
                    locations="Abbreviation",  # column with locations
                    color="Color",  # column with color values
                    color_discrete_map=colors,  # custom colormap
                    locationmode="USA-states")  # plot as Us states

fig.update_layout(geo_scope="usa")  # plot only usa
fig.show()
