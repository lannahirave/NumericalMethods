
double  e = Pow(10, -6), a = -2, b = 2;
double  x[13] = { -2, -5.0/3.0, -4.0 / 3.0, -1, -2.0 / 3.0, -1.0 / 3.0, 0, 1.0 / 3.0, 2.0 / 3.0, 1, 4.0 / 3.0, 5.0 / 3.0, 2 }, 
f[13], dd[12][12];

double polynom(double x);
void initF();
void solve();
void printResult();

int main() {
    initF();
    solve();
    printResult();
    return 0;
}


double polynom(double x) {
    return Pow(x, 15) * (4 + x * (2 * x * x - 1));
}

void initF() {
    for (int i = 0; i < 13; i++)
        f[i] = polynom(x[i]);
}

void solve() {
    for (int i = 0; i < 12; i++)
        dd[i][0] = (f[i + 1] - f[i]) / (x[i + 1] - x[i]);

    for (int j = 1; j < 12; j++) {
        for (int i = 0; i < 12 - j; i++) {
            dd[i][j] = (dd[i + 1][j - 1] - dd[i][j - 1]) / (x[i + j] - x[i]);
        }
    }
}

void printResult() {
    double c;
    double result[13], coef[13];
    for (int i = 0; i < 13; i++)
        result[i] = 0;
    result[0] = f[0];
    for (int j = 0; j < 12; j++) {
        c = round(dd[0][j] / e) * e + 0.0;
        if (j == 0) {
            coef[0] = (-1) * x[0];
            coef[1] = 1;
            for (int t = 2; t < 13; t++)
                coef[t] = 0;
        }
        else {
            for (int i = j; i > 0; i--) {
                coef[i] = coef[i - 1] - coef[i] * x[j];
            }
            coef[0] = coef[0] * (-1) * x[j];
            coef[1 + j] = 1;
        }
        for (int t = 1; t < 13; t++)
            result[t] += c * coef[t];
    }

    cout << "P(x) ~ " << fixed << setprecision(log10(1.0 / e)) << result[0];
    for (int i = 1; i < 13; i++) {
        if (result[i] < 0)
            cout << " " << result[i];
        else
            cout << " + " << result[i];
        cout << "x^" << i;
    }

    cout << endl; 
}