using System.Text.RegularExpressions;

namespace Recharge.Application.Validator;
public class CustomUserValidator {

    // Método para verificar se a senha atende aos requisitos de segurança
    public static bool SecurityLevel(string password) {
        // Definição das constantes para os requisitos da senha
        const int MinimumLength = 8;
        const string UppercasePattern = @"[A-Z]";
        const string LowercasePattern = @"[a-z]";
        const string DigitPattern = @"\d";
        const string SpecialCharacterPattern = @"[!@#$%^&*()]";

        // Verifica o tamanho mínimo da senha
        if (password.Length < MinimumLength) {
            return false;
        }

        // Verifica se a senha contém pelo menos uma letra maiúscula
        if (!Regex.IsMatch(password, UppercasePattern)) {
            return false;
        }

        // Verifica se a senha contém pelo menos uma letra minúscula
        if (!Regex.IsMatch(password, LowercasePattern)) {
            return false;
        }

        // Verifica se a senha contém pelo menos um dígito numérico
        if (!Regex.IsMatch(password, DigitPattern)) {
            return false;
        }

        // Verifica se a senha contém pelo menos um caractere especial
        if (!Regex.IsMatch(password, SpecialCharacterPattern)) {
            return false;
        }

        // Se todas as verificações passarem, a senha atende aos requisitos de segurança
        return true;
    }

    // Método para validar o CPF
    public static bool CpfValidator(string cpf) {
        // Remove todos os caracteres não numéricos do CPF
        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        // Verifica se o CPF tem o tamanho correto (11 dígitos)
        if (cpf.Length != 11) {
            return false;
        }

        // Verifica se todos os dígitos do CPF são iguais (CPF inválido)
        if (cpf.All(digit => digit == cpf[0])) {
            return false;
        }

        // Pesos utilizados no cálculo dos dígitos verificadores do CPF
        int[] weights1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] weights2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        // Variáveis para calcular os dígitos verificadores do CPF
        int sum1 = 0;
        int sum2 = 0;

        // Calcula a soma ponderada dos primeiros 9 dígitos do CPF
        for (int i = 0; i < 9; i++) {
            sum1 += int.Parse(cpf[i].ToString()) * weights1[i];
            sum2 += int.Parse(cpf[i].ToString()) * weights2[i];
        }

        // Calcula o primeiro dígito verificador
        int remainder1 = sum1 % 11;
        int digit1 = remainder1 < 2 ? 0 : 11 - remainder1;

        // Calcula o segundo dígito verificador
        int remainder2 = sum2 % 11;
        int digit2 = remainder2 < 2 ? 0 : 11 - remainder2;

        // Verifica se os dígitos verificadores do CPF são válidos
        if (int.Parse(cpf[9].ToString()) != digit1 || int.Parse(cpf[10].ToString()) != digit2) {
            return false;
        }

        // Se todas as verificações passarem, o CPF é válido
        return true;
    }

    // Método para validar o número de telefone
    public static bool PhoneValidator(string phoneNumber) {
        // Remove todos os caracteres não numéricos do número de telefone
        phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

        // Verifica se o número de telefone tem o tamanho mínimo de um número de telefone (DDD + número)
        if (phoneNumber.Length >= 10) {
            // Remove o "0" inicial do DDD, se presente
            if (phoneNumber[0] == '0') {
                phoneNumber = phoneNumber.Substring(1);
            }

            // Formata o número no padrão DDD + número (sem espaços, hífen ou parenteses)
            phoneNumber = $"{phoneNumber.Substring(0, 2)}{phoneNumber.Substring(2)}";

            // Se todas as verificações passarem, o número de telefone é válido
            return true;
        }

        // Caso o número não tenha o tamanho mínimo, é considerado inválido
        return false;
    }
}