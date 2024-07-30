// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getFunctions, httpsCallable } from "firebase/functions";


const firebaseConfig = {
  apiKey: "AIzaSyCSXpyzeSJzaGqrKiNFRPXRWB8nmROCLdE",
  authDomain: "clouderp-93d91.firebaseapp.com",
  projectId: "clouderp-93d91",
  storageBucket: "clouderp-93d91.appspot.com",
  messagingSenderId: "370954722736",
  appId: "1:370954722736:web:b43f28fb7f32d8cfdd8a75",
  measurementId: "G-CE1KQ6G9NT"
};

// Initialize Firebase
const firebaseApp = initializeApp(firebaseConfig);

const functions = getFunctions(firebaseApp);

export { firebaseApp, functions, httpsCallable };
