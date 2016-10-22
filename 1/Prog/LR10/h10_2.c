/*
Входные данные: целые знаковые числа x и y.
Выходные данные:
	1) дополнительные коды для входных чисел x', y'.
	2) произведение чисел x' и y' согласно правилам арифметики чисел, представленных дополнительным кодом.
	3) знаковое представление полученного результата умножения.
Рекомендации:
	1) выполнить преобразование входных знаковых чисел в дополнительный код;
	2) реализовать алгоритм умножения, учитывая, что: x·y = s·|x|·|y|, где s – знак получаемого числа, s = 1, если знаки x и y одинаковы и s = -1 в противном случае;
	3) результат умножения s·|x|·|y| должен принадлежать диапазону допустимых значений данного дополнительного кода, если это не так – выполнить приведение по правилу упражнения C09.4;
	4) выполнить преобразование полученного результата из дополнительного кода в знаковое число согласно алгоритму решения задачи 3.4.
*/

#include <stdio.h>

int main()
{
	int x = 0, y = 0, xOLD = 0, yOLD = 0, a = 0;
	printf("Введите X и Y: ");
	scanf("%d%d", &x, &y);
	
	if (x > 4999 || x <= -5000 || y > 4999 || y <= -5000)
		printf("Ошибка данных\n");
	else
	{
		if (x >= 0)
			xOLD = x;
		if (y >= 0)
			yOLD = y;

		if (x < 0)
			xOLD = 10000 - (((x >= 0) - (x < 0)) * x);
		if (y < 0)
			yOLD = 10000 - (((y >= 0) - (y < 0)) * y);

		printf("Дополнительные коды для входных чисел: X`=%d\tY`=%d\n", xOLD, yOLD);
			
		a = yOLD / xOLD;
		if (a >= 10000)
			while (a >= 9999)
				a = a - 10000;
			
		if ((a >= 5000) && (x > 0) && (y > 0))
			a = 4999;
		printf("Частное чисел в доп. коде %d\n", a);

		if ((0 <= a) && (a < 5000))
			printf("Частное со знаком: %d\n", a);
		if ((a >= 5000) && (a < 10000)) 
		{
			a=(-1) * (10000 - a);
			printf("Частное со знаком: %d\n", a);
		}
	}
	return 0;
}
