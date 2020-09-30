import matplotlib.pyplot as plt
import numpy as np
from sklearn.linear_model import LinearRegression

rng = np.random.RandomState(42)
x = 10 * rng.rand(50)
y = 2 * x - 1 + rng.randn(50)

plt.scatter(x, y)

model = LinearRegression(fit_intercept=True)

X = x[:, np.newaxis]

model.fit(X, y)

print('coef = ', model.coef_)
print('intercept = ', model.intercept_)

xfit = np.linspace(-1, 11)
Xfit = xfit[:, np.newaxis]
yfit = model.predict(Xfit)

plt.scatter(x, y)
plt.plot(xfit, yfit)

plt.show()
