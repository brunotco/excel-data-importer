export class User {
    id: number = 0;
    fullname: string | null = null;
    username: string | null = null;
    password: string | null = null;
    email: string | null = null;
    active: boolean | null = null;

    constructor(fullname: string = '', username: string = '', password: string = '', email: string = '', active = true) {
        this.fullname = String(fullname);
        this.username = String(username);
        this.password = String(password);
        this.email = String(email);
        this.active = active;
    }
}