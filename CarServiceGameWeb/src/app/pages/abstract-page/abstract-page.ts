export class AbstractPage {
    private loading;
    
    getOpacity(): number {
        if (this.loading) {
            return 0.4;
        } else {
            return 1;
        }
    }

    isLoading(): boolean {
        return this.loading;
    }

    setLoading(value:boolean) {
        this.loading = value;
    }
}