/*
Входные данные: вещественное число x.
Выходные данные: модуль числа x.
Рекомендации:
	Для вычисления модуля можно использовать соотношение:	((x >= 0) – (x < 0)) * x
*/

#include <stdio.h>

int main()
{
	double a;
	printf("Введите X: ");
	scanf("%lf", &a);
	printf("\t|X| = %.2lf\n", ((a >= 0) - (a < 0)) * a);
	return 0;
}


