
const getToken = () => {
    return localStorage.getItem('token');
};

const isExpired = (): boolean => {
    const expiresIn = localStorage.getItem('expiresDate');
    if (expiresIn === null || isNaN(Date.parse(expiresIn))) {
      return false;
    }

    const expDate: Date = new Date(expiresIn);
    return new Date().getTime() > expDate.getTime();
};

const isAuthenticated = () => {
    return !!getToken() && !isExpired();
};


export default {
    getToken,
    isExpired,
    isAuthenticated
};
