// ======= Funções utilitárias =======
function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return decodeURIComponent(parts.pop().split(';').shift());
    return null;
}

function setCookie(name, value, days = 7) {
    const expires = new Date(Date.now() + days * 864e5).toUTCString();
    document.cookie = `${name}=${encodeURIComponent(value)}; expires=${expires}; path=/`;
}

// ======= Manipulação do carrinho =======
function getCart() {
    const cookie = getCookie("semprebella_cart");
    if (!cookie) return [];

    try {
        let cart = JSON.parse(cookie);

        // 🔧 Corrige valores que vierem como string
        cart.forEach(item => {
            item.price = parseFloat(item.price);
            item.quantity = parseInt(item.quantity);
        });

        return cart;
    } catch (e) {
        console.error("Erro ao ler o carrinho:", e);
        return [];
    }
}

function saveCart(cart) {
    setCookie("semprebella_cart", JSON.stringify(cart));
    updateCartCount();
}

function addToCart(product) {
    let cart = getCart();
    let existing = cart.find(x => x.id === product.id);

    if (existing) {
        existing.quantity++;
    } else {
        cart.push({ ...product, quantity: 1 });
    }

    saveCart(cart);
    updateCartCount();

    // Mostra alerta visual (sem travar)
    const alertDiv = document.createElement("div");
    alertDiv.className = "alert alert-success position-fixed top-0 end-0 m-3 shadow";
    alertDiv.innerHTML = `✅ <strong>${product.name}</strong> foi adicionado ao carrinho!`;
    document.body.appendChild(alertDiv);
    setTimeout(() => alertDiv.remove(), 2000);
}

function removeFromCart(productId) {
    let cart = getCart().filter(p => p.id !== productId);
    saveCart(cart);
    renderCart();
}

function updateQuantity(productId, delta) {
    let cart = getCart();
    let item = cart.find(p => p.id === productId);
    if (!item) return;

    item.quantity += delta;

    // 🗑️ Se a quantidade chegou a 0 ou menos, remove o item
    if (item.quantity <= 0) {
        cart = cart.filter(p => p.id !== productId);
    }

    saveCart(cart);
    renderCart();
}

// ======= Atualiza contador no botão do carrinho =======
function updateCartCount() {
    let cart = getCart();
    let count = cart.reduce((acc, item) => acc + item.quantity, 0);
    let badge = document.getElementById("cart-count");
    if (badge) badge.innerText = count;
}

// ======= Renderização dos itens no modal =======
function renderCart() {
    let cart = getCart();
    console.log("📦 Conteúdo do carrinho carregado do cookie:", cart); // 🔍 DEPURAÇÃO
    let container = document.getElementById("cart-items");
    let totalSpan = document.getElementById("cart-total");
    container.innerHTML = "";
    let total = 0;

    if (cart.length === 0) {
        container.innerHTML = `<p class="text-center text-muted">Carrinho vazio 🛍️</p>`;
        totalSpan.innerText = "R$0,00";
        return;
    }

    cart.forEach(item => {
        let subtotal = item.price * item.quantity;
        total += subtotal;

        container.innerHTML += `
        <div class="d-flex align-items-center justify-content-between mb-3 border-bottom pb-2">
            <div class="d-flex align-items-center">
                <img src="${item.image}" alt="${item.name}" class="me-2" 
                     style="width: 70px; height: 70px; object-fit: cover; border-radius: 10px;">
                <div>
                    <strong>${item.name}</strong><br>
                    <span class="text-muted">R$${item.price.toFixed(2)} cada</span>
                </div>
            </div>
            <div class="text-end">
                <div class="d-flex align-items-center justify-content-end mb-1">
                    <button class="btn btn-sm btn-outline-secondary me-2" onclick="updateQuantity(${item.id}, -1)">-</button>
                    <span>${item.quantity}</span>
                    <button class="btn btn-sm btn-outline-secondary ms-2" onclick="updateQuantity(${item.id}, 1)">+</button>
                </div>
                <span class="fw-bold text-success">R$${subtotal.toFixed(2)}</span>
                <button class="btn btn-sm btn-link text-danger d-block mt-1" onclick="removeFromCart(${item.id})">Remover</button>
            </div>
        </div>
        `;
    });

    totalSpan.innerText = "R$" + total.toFixed(2);
}

// ======= Inicialização =======
document.addEventListener("DOMContentLoaded", updateCartCount);
