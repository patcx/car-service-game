import { DomSanitizer, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';

export class Order {
    orderId: string;
    carName: string;
    requiredWork: number;
    reward: number;
    description: string;

    constructor(id,carName,requiredWork,reward,description) {
        this.orderId = id;
        this.carName = carName;
        this.requiredWork = requiredWork;
        this.reward = reward;
        this.description = description;
    }
}