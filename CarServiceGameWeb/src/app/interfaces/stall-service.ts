import { Stall } from "../model/stall";

export interface IStallService {
    createStalls();
    getStalls(): Array<Stall>;
}