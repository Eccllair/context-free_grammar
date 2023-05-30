int global_var, global_var_2 = 0;
bool passing_flag_1 = true;
bool passing_flag_2 = false;

int Factorial(int num = 5) {
    int i = 1;
    int result = 1;
    while (i <= num && (passing_flag_1 || passing_flag_2)) {
        result = result * i;
        i = i + 1;
    }
    return result;
}

int Start(int factorial) {
    return factorial;
}

int Main() {
    return Start(Factorial());
}

