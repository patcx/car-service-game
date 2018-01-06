export interface IPaymentService {
    sendCode(email);
    useCode(code);
}
