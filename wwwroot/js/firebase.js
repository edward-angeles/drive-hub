import {intializeApp} from "firebase/app";
import {
    getAuth,
    signInWithEmailAndPassword,
    createUserWithEmailAndPassword,
    signOut,
    onAuthStateChanged,

} from "firebase/auth";

const firebaseConfig = {
  apiKey: "AIzaSyDzQz3gMB04NOgnTLSwDnaML8YlR83drKU",
  authDomain: "drivehub-5c7a4.firebaseapp.com",
  projectId: "drivehub-5c7a4",
  storageBucket: "drivehub-5c7a4.firebasestorage.app",
  messagingSenderId: "488642303453",
  appId: "1:488642303453:web:be86dab2fafcde410c1234",
  measurementId: "G-FRSCMT4M8C"
};
const app = initializeApp(firebaseConfig);
const auth = getAuth(app);
window.firebaseAuth = {
    auth,
    signInWithEmailAndPassword,
    createUserWithEmailAndPassword,
    signOut,
    onAuthStateChanged
};
export {auth, signInWithEmailAndPassword, createUserWithEmailAndPassword, signOut, onAuthStateChanged};